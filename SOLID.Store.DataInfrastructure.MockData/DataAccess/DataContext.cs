using System;
using System.Collections.Generic;
using System.Text;
using SOLID.Store.DataAccess;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.DataInfrastructure.MockData.DataAccess
{
    class DataContext: IDataContext
    {
        private DataContext() : base() { }
        private static DataContext m_Current;
        internal static DataContext Current {
            get {
                if (m_Current == null) m_Current = new DataContext();
                return m_Current;
            }
        }

        internal void ClearAccessors() => m_dicAccessors = null;

        private Dictionary<Type, IMockDataAccessor> m_dicAccessors;
        internal void RegisterAccessor(IMockDataAccessor value) {
            if (m_dicAccessors == null) m_dicAccessors = new Dictionary<Type, IMockDataAccessor>();
            m_dicAccessors[value.dtoType] = value;
        }

        internal IMockDataAccessor FindAccessor<T>() where T : pocoBase => FindAccessor(typeof(T));
        internal IMockDataAccessor FindAccessor(Type pocoType)
        {
            if (m_dicAccessors != null && m_dicAccessors.TryGetValue(pocoType, out var accessor)) return accessor;
            return null;
        }

        public void BeginTransaction() {
            if (m_dicAccessors != null && m_dicAccessors.Count > 0)
            {
                foreach (var kv in m_dicAccessors)
                {
                    kv.Value.BeginTransaction();
                }
            }

        }       

        public void CommitTransaction()
        {
            if (m_dicAccessors != null && m_dicAccessors.Count > 0) {
                foreach (var kv in m_dicAccessors) {
                    kv.Value.CommitChanges();
                }
            }
        }

        public void SaveChanges()
        {
            //do nothing for mock data accessors
        }

        public void RollBackTransaction()
        {
            if (m_dicAccessors != null && m_dicAccessors.Count > 0)
            {
                foreach (var kv in m_dicAccessors)
                {
                    kv.Value.RollBackChanges();
                }
            }
        }
    }
}
