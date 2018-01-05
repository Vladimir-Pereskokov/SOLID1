using System;
using System.Collections.Generic;
using SOLID.Store.Entities.Repo;
using SOLID.Store.DataAccess;
using SOLID.Store.Entities.IO;

namespace SOLID.Store.ModuleInitializers.Concrete
{
    public class PurchaseOrderEntityInitializer : EntityInitializer<PurchaseOrder>
    {
        public PurchaseOrderEntityInitializer(DataAccess.IDataAccessor purchaseOrderAccessor) : base(purchaseOrderAccessor) { }
        protected override Func<PurchaseOrder> InstanceCreator() => CreateOrder;
        private static PurchaseOrder CreateOrder() => new PurchaseOrder();
    }
}
