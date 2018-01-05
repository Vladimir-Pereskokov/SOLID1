using System;
using System.Collections.Generic;
using System.Text;
using SOLID.Store.ModuleInitializers;
using SOLID.Store.Entities;
using SOLID.Store.Entities.Repo;

namespace SOLID.Store.ModuleInitializers.Concrete
{
    public class CustomerInitializer : EntityInitializer<Customer>
    {
        public CustomerInitializer(DataAccess.IDataAccessor customerAccessor) : base(customerAccessor) { }
        protected override Func<Customer> InstanceCreator() => CreateCustomer;
        private static Customer CreateCustomer() => new Customer();
    }
    
}
