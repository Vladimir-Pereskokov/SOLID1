using System;
using System.Collections.Generic;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.DataAccess;
using SOLID.Store.DTOs.BaseDTO;
using SOLID.Store.Entities.IO;




namespace SOLID.Store.ModuleInitializers
{

    /// <summary>
    /// Is used to describe a module containing entities that describe business logic
    /// </summary>
    public abstract class Initializer
    {
        private DataAccessService m_DataAccessSvc;
        private IOFacadeService m_FacadeSvc;

        private readonly IEnumerable<EntityInitializer> m_entityInits;
        protected Initializer(IEnumerable<EntityInitializer> initializers) : base() { m_entityInits = initializers;}

        protected DataAccessService DataAccessSvc => this.m_DataAccessSvc;
        protected IOFacadeService FacadeSvc => this.m_FacadeSvc;

        protected virtual void OnAfterInit() {}

        protected internal virtual IEnumerable<EntityInitializer> GetEntityInitializers() => m_entityInits;

        public static void Init(IEnumerable<Initializer> values)
        {
            if (values != null)
            {
                foreach (var iz in values)
                {
                    var entInits = iz.GetEntityInitializers();
                    if (entInits != null)
                    {
                        foreach (var entInit in entInits)
                        {
                            var entCreator = entInit.Creator();
                            if (entCreator != null) {
                                var testEnt = entCreator();
                                if (testEnt != null) {
                                    iz.m_FacadeSvc = IOFacadeService.Current;
                                    iz.m_FacadeSvc.CheckIn(testEnt.GetType(), entCreator);
                                    if (entInit.m_dataAccessor != null) {
                                        iz.m_DataAccessSvc = DataAccessService.Current;
                                        iz.m_DataAccessSvc.AddDataAccessor(testEnt, entInit.m_dataAccessor);
                                    }
                                        
                                }                                
                            }                            
                        }
                    }
                    iz.OnAfterInit();
                }
            }
        }
    }
    public abstract class EntityInitializer
    {
        internal readonly IDataAccessor m_dataAccessor = null;
        protected EntityInitializer(IDataAccessor dataAccessor) : base() { m_dataAccessor = dataAccessor; }
        internal abstract Func<Entity> Creator();
    }


    public abstract class EntityInitializer<T> : EntityInitializer where T : Entity
    {
        protected EntityInitializer(IDataAccessor dataAccessor) : base(dataAccessor) { }


        /// <summary>
        /// Must return an instance of the Entity
        /// </summary>
        /// <returns></returns>
        protected abstract Func<T> InstanceCreator();

        internal override Func<Entity> Creator() => InstanceCreator();
    }
}

