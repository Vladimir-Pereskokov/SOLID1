using System;
using System.Collections.Generic;
using SOLID.Store.Entities.Repo;
using SOLID.Store.DataAccess;
using SOLID.Store.Entities.IO;

namespace SOLID.Store.ModuleInitializers.Concrete
{
    public class PurchaseOrderItemEntityInitializer : EntityInitializer<PurchaseOrderItem>
    {
        public PurchaseOrderItemEntityInitializer(DataAccess.IDataAccessor purchaseOrderItemAccessor) : base(purchaseOrderItemAccessor) { }
        protected override Func<PurchaseOrderItem> InstanceCreator() => CreateOrderItem;
        private static PurchaseOrderItem CreateOrderItem() => new PurchaseOrderItem();
    }
}
