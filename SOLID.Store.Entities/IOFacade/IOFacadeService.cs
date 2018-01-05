using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.DataAccess;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.Entities.IO {

    public class IOFacadeService {
        private Dictionary<Type, Func<Entity>> m_Dic = null;

        private Dictionary<Type, Func<Entity>> Dic { 
            get {
            if (m_Dic == null) m_Dic = new Dictionary<Type, Func<Entity>>();
            return m_Dic;
            }
        }

        private IOFacadeService(): base() {}

        private static IOFacadeService m_Current = null;
        public static IOFacadeService Current {
            get {
                 if (m_Current == null) m_Current = new IOFacadeService();
                 return m_Current;}
        }
        internal void CheckIn(Type entType , Func<Entity> creator){
            if (creator != null && entType != null) {                
                if (creator  != null) this.Dic[entType] = creator;
            }
        }

        internal Func<Entity> GetCreator(Type entityType) {
            if (m_Dic != null && entityType != null ) {
                if (m_Dic.TryGetValue(entityType, out var fnc)) return fnc;                
            }
            return null;
        }


        public T CreateNew<T>(pocoBase dto) where T : Entity
        {
            var obj =  (T)CreateNew(typeof(T));
            if (dto != null) {
                if (obj.m_dto.GetType() != dto.GetType()) throw new ArgumentException("Invalid dto type");
                
                obj.m_dto = dto;

            }
            return obj;
        }


        public T CreateNew<T>() where T:Entity {
            return (T)CreateNew(typeof(T));
        }

        private Entity CreateNew(Type t) {
            var ent = CreateInstance(t);
            if (ent != null && !ent.CanCreateNew()) throw new Authorization.AuthorizationException();
            else return ent;
        }


        private Entity CreateInstance(Type entType) {
            if (entType != null)
            {
                var creator = GetCreator(entType);
                if (creator != null) return creator();                
            }
            return null;
        }

        /// <summary>
        /// Reads entity object
        /// </summary>
        /// <typeparam name="T">type of the entity to read</typeparam>
        /// <param name="id">id (primary key) of the entity to read</param>
        /// <returns></returns>
        public T ReadEntity<T>(Guid id) where T:Entity {
            var lst = ReadEntityList<T>(SearchCriteria.CreateForEntity<T>(id));
            if (lst != null && lst.Count > 0) return lst[0];
            return null;
        }

        /// <summary>
        /// Reads entity objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <returns>list of entity objects found</returns>
        public List<T> ReadEntityList<T>(SearchCriteria criteria) where T: Entity {
            if (criteria == null) throw new ArgumentNullException("Search criteria is required");
            var lst = new List<T>();
            T result = (T)CreateInstance(typeof(T));
            if (result != null)
            {
                if (!result.CanRead()) throw new Authorization.AuthorizationException();
                try
                {
                    var accessor = DataAccessService.Current.GetAccessor(result.m_dto.GetType());
                    if (accessor != null)
                    {
                        var dtoItems = accessor.Read(criteria);
                        if (dtoItems != null)
                        {
                            foreach (var dto in dtoItems)
                            {
                                var existItem = lst.Count == 0 ? result : (T)CreateInstance(typeof(T));
                                existItem.m_dto = dto;
                                lst.Add(existItem);
                            }
                        }
                    }
                }
                catch { throw; }
            }
            else throw new ArgumentException($"Cannot create instance of type {typeof(T).Name}");
            return lst;
        }

        public void Update(IEnumerable<Entity> entities) {
            if (entities == null || !entities.Any()) throw new DataAccessException("Nothing to update");
            var dic = new Dictionary<IDataContext, bool>();
            bool bSuccess = false;
            Exception exResult = null;
            try
            {
                foreach (var e in entities)
                {
                    if (e != null) {                        
                        if (e.IsDirty) {
                            e.Validate();
                            if (!e.IsValid) throw new Validation.InvalidEntityException(e);
                            if (!e.CanSave()) throw new Exceptions.CantSaveException(e);
                            var entAccess = DataAccessService.Current.GetEntityDataAccessor(e.GetType());
                            if (entAccess == null) throw new NoDataAccessorException(e.GetType());
                            var cxt = entAccess.Context;
                            if (cxt == null) throw new DataAccessException($"No Data Cotext provided for {entAccess.dtoType.Name}");
                            if (!dic.ContainsKey(cxt))
                            {
                                cxt.BeginTransaction();
                                dic.Add(cxt, true);
                            }
                            if (e.IsDeleted) entAccess.Delete(new pocoBase[] { e.m_dto });
                            else if (e.IsNew) entAccess.Create(new pocoBase[] { e.m_dto });
                            else entAccess.Update(new pocoBase[] { e.m_dto });
                        }                                               
                    }
                }
                bSuccess = dic.Count > 0;
                if (!bSuccess) throw new DataAccessException("Nothing to update");
            }
            catch (Exception ex) {
                exResult = ex; bSuccess = false;
            }
            finally
            {
                if (dic.Count > 0)
                {
                    try
                    {
                        if (bSuccess)
                        {
                            foreach (var kv in dic)
                            {
                                if (kv.Value)
                                {
                                    kv.Key.SaveChanges();
                                }
                            }
                        }
                    }
                    catch (Exception exSave)
                    {
                        if (bSuccess) exResult = exSave; bSuccess = false;
                    }
                    finally {
                        try
                        {
                            foreach (var kv in dic)
                            {
                                if (kv.Value)
                                {
                                    if (bSuccess) kv.Key.CommitTransaction();
                                    else kv.Key.RollBackTransaction();
                                }
                            }
                            if (bSuccess) foreach (var e in entities) { if (!e.IsDeleted) e.m_dto.MarkClean();}
                        }
                        catch (Exception ex) {
                            if (exResult == null) exResult = ex;
                        }                        
                    }
                }                
            }
            if (exResult != null) throw exResult;
        }

        



    }

}