using System;
using System.Linq;
using System.Collections.Generic;
using SOLID.Store.DTOs.BaseDTO;
using SOLID.Store.Entities;
using SOLID.Store.Entities.BaseEntities;


namespace SOLID.Store.DataAccess {

    public sealed class DataAccessService {
        private Dictionary<Type, IDataAccessor> m_ormItemAccessors = null;
        private Dictionary<Type, Type> m_EntityToOrmTypeConverter = null;
        
        private static DataAccessService m_Current = null;

        private DataAccessService(): base() {}

        internal static DataAccessService Current { 
            get {if (m_Current == null) m_Current = new DataAccessService(); 
            return m_Current;}
        }

        internal IDataAccessor GetEntityDataAccessor(Type entityType) {
            if (entityType != null && m_EntityToOrmTypeConverter != null) {
                if (this.m_EntityToOrmTypeConverter.TryGetValue(entityType, out var dtoType)) return GetAccessor(dtoType);
            }
            return null;
        }


        internal Type ResolveDtoType(Type entityType) {
            if (entityType != null && m_EntityToOrmTypeConverter != null)
            {
                if (this.m_EntityToOrmTypeConverter.TryGetValue(entityType, out var dtoType)) return dtoType;
            }
            return null;
        }
        internal Type ResolveDtoType<E>() where E : Entity => ResolveDtoType(typeof(E));



        internal IDataAccessor GetAccessor(Type dtoType) {
                if (m_ormItemAccessors != null) {
                if (m_ormItemAccessors.TryGetValue(
                    dtoType, out var acc)) return acc;
                }            
                return null;
        }

        internal IDataAccessor GetAccessor<T>() where T : pocoBase => GetAccessor(typeof(T));
               
        internal void AddDataAccessor(Entity entity, IDataAccessor value) {
            if (value != null && entity != null){
                var dtoType = entity.m_dto.GetType();
                var entType = entity.GetType();
                if (m_ormItemAccessors == null)
                {
                    m_ormItemAccessors = new Dictionary<Type, IDataAccessor>();
                    m_EntityToOrmTypeConverter = new Dictionary<Type, Type>();
                }
                m_ormItemAccessors[dtoType] = value;
                m_EntityToOrmTypeConverter[entity.GetType()] = dtoType;
            }
        }

        public T GetDto<T>(Guid id) where T : pocoBase => GetDto<T>(SearchCriteria.Create<T>(id));

        public T GetDto<T>(SearchCriteria criteria) where T : pocoBase
        {
            var pocoObjs = GetDtoList<T>(criteria);
            if (pocoObjs != null && pocoObjs.Any()) return (T)pocoObjs.First();
            return null;
        }

        public IEnumerable<pocoBase> GetDtoList<T>(SearchCriteria criteria) where T : pocoBase
        {
            if (criteria != null) {
                var accessor = GetAccessor(typeof(T));
                if (accessor != null) return accessor.Read(criteria);                
            }
            return null;
        }

    }
}