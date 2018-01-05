using System;
using SOLID.Store.DTOs;
using SOLID.Store.DataInfrastructure.MockData.DataAccess;

namespace SOLID.Store.DataInfrastructure.MockData.DataAccess.Accessors
{
    internal class CustomerDataAccessor : MockDataAccessor<CustomerDTO> {
        protected override void OnInitRecords() => Records.AddRange(MockCustomers.GetCustomers());
    }
}
