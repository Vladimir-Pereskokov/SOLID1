using System;
using SOLID.Store.DataAccess;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.DataInfrastructure.MockData.DataAccess
{
    internal interface IMockDataAccessor: IDataAccessor 
    {
        void AddNewItem(pocoBase item);
        void AddUpdateItem(pocoBase item);
        void AddDeleteID(Guid id);
        void BeginTransaction();
        void CommitChanges();
        void RollBackChanges();
        pocoBase Find(Guid id);
    }
}
