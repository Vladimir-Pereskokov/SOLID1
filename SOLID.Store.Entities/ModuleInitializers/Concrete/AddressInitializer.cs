using System;
using SOLID.Store.ModuleInitializers;
using SOLID.Store.Entities.Repo;

namespace SOLID.Store.ModuleInitializers.Concrete
{
    public class AddressInitializer: EntityInitializer<Address>
    {
        public AddressInitializer(DataAccess.IDataAccessor customerAccessor) : base(customerAccessor) { }
        protected override Func<Address> InstanceCreator() => CreateAddress;
        private static Address CreateAddress() => new Address();
    }
}
