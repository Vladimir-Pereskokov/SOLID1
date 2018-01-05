using System;
using System.Collections.Generic;
using SOLID.Store.Entities.Repo;
using SOLID.Store.DataAccess;
using SOLID.Store.Entities.IO;

namespace SOLID.Store.ModuleInitializers.Concrete
{
    public class ShippingSlipEntityInitializer : EntityInitializer<ShippingSlip>
    {
        public ShippingSlipEntityInitializer(DataAccess.IDataAccessor shippingSlipAccessor) : base(shippingSlipAccessor) { }
        protected override Func<ShippingSlip> InstanceCreator() => CreateSlip;
        private static ShippingSlip CreateSlip() => new ShippingSlip();
    }
}
