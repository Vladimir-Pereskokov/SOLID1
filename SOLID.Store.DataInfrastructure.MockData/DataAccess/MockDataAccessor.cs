using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SOLID.Store.DataAccess;
using SOLID.Store.DTOs.BaseDTO;
using SOLID.Store.DTOs;

namespace SOLID.Store.DataInfrastructure.MockData.DataAccess
{
    internal abstract class MockDataAccessor<T> : IMockDataAccessor where T: pocoBase
    {
        internal MockDataAccessor() : base() { DataContext.Current.RegisterAccessor(this); }
        public Type dtoType => typeof(T);
        public IDataContext Context => DataContext.Current;

        public void Create(IEnumerable<pocoBase> items)
        {
            foreach (var itm in items) {
                var accessor = DataContext.Current.FindAccessor(itm.GetType());
                if (accessor != null) accessor.AddNewItem(itm);
            }
        }

        public void Delete(IEnumerable<pocoBase> items)
        {
            foreach (var itm in items)
            {
                var accessor = DataContext.Current.FindAccessor(itm.GetType());
                if (accessor != null) accessor.AddDeleteID(itm.ID);
            }
        }

        public virtual IEnumerable<pocoBase> Read(SearchCriteria criteria)
        {
            return (criteria.ID != Guid.Empty) ? (from r in Records where r.ID == criteria.ID select r) : Records;            
        }


        public void Update(IEnumerable<pocoBase> items) {
            foreach (var itm in items)
            {
                var accessor = DataContext.Current.FindAccessor(itm.GetType());
                if (accessor != null) accessor.AddUpdateItem(itm);
            }
        }

        private void CleanUp() {
            m_DeleteIDs = null;
            m_UpdateItems = null;
            m_NewItems = null;
            if (m_Records != null) foreach (var itm in m_Records) itm.MarkClean();
        }

        public pocoBase Find(Guid id) => Records.FirstOrDefault(item => item.ID == id);

        public virtual void BeginTransaction() { }

        public void RollBackChanges() {
            CleanUp();
        }
        public void CommitChanges() {
            if (m_DeleteIDs != null) {
                foreach (var id in m_DeleteIDs) {
                    var existItem = Find(id);
                    if (existItem != null) Records.Remove(existItem as T);
                }
            }
            if (m_UpdateItems != null) {
                foreach (var item in m_UpdateItems)
                {
                    var existItem = Find(item.ID);
                    if (existItem != null) Records.Remove(existItem as T);
                }
                foreach (var item in m_UpdateItems) Records.Add(item as T);
            }
            if (m_NewItems != null) foreach (var item in m_NewItems) Records.Add(item as T);

            CleanUp();
        }

        private List<Guid> m_DeleteIDs = null;
        public void AddDeleteID(Guid id)
        {
            if (m_DeleteIDs == null) m_DeleteIDs = new List<Guid>();
            if (!m_DeleteIDs.Contains(id)) m_DeleteIDs.Add(id);
        }

        private List<pocoBase> m_UpdateItems = null;
        public void AddUpdateItem(pocoBase item)
        {
            if (m_UpdateItems == null) m_UpdateItems = new List<pocoBase>();
            if (!m_UpdateItems.Contains(item)) m_UpdateItems.Add(item);
        }

        private List<pocoBase> m_NewItems = null;
        public void AddNewItem(pocoBase item)
        {
            if (m_NewItems == null) m_NewItems = new List<pocoBase>();
            if (!m_NewItems.Contains(item)) m_NewItems.Add(item);
        }

        private List<T> m_Records;
        protected internal List<T> Records {
            get {
                if (m_Records == null) { m_Records = new List<T>(); OnInitRecords(); }
                return m_Records;
            }
        }
        protected virtual void OnInitRecords() { }

    }
}
