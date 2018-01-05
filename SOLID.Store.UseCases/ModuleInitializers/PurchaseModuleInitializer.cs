using System;
using System.Collections.Generic;
using SOLID.Store.Entities.Repo;
using SOLID.Store.DataAccess;
using SOLID.Store.Entities.IO;


namespace SOLID.Store.ModuleInitializers.Concrete {
    public class PurchaseModuleInitializer: Initializer {
        internal static PurchaseModuleInitializer Current;
        public PurchaseModuleInitializer(IEnumerable<EntityInitializer> initializers) : base(initializers) { }

        protected override void OnAfterInit() { base.OnAfterInit();
                                                    Current = this;
        }

        internal IOFacadeService FacadeS => base.FacadeSvc;
        internal DataAccessService DAccessSvc => base.DataAccessSvc;

    }

    public class ProductInitializer : EntityInitializer<Product>
    {
        private ProductInitializer(IDataAccessor productAccessor) : base(productAccessor) { }
        private static Product CreateProduct() => new Product();
        protected override Func<Product> InstanceCreator() => CreateProduct;
    }


    public class PurchaseOrderInitializer : EntityInitializer<PurchaseOrder>
    {
        private PurchaseOrderInitializer(IDataAccessor purchaseOrderAccessor) : base(purchaseOrderAccessor) { }
        private static PurchaseOrder CreateOrder() => new PurchaseOrder();
        protected override Func<PurchaseOrder> InstanceCreator() => CreateOrder;
    }

    public class PurchaseOrderItemInitializer : EntityInitializer<PurchaseOrderItem>
    {
        private PurchaseOrderItemInitializer(IDataAccessor purchaseOrderItemAccessor) : base(purchaseOrderItemAccessor) { }
        private static PurchaseOrderItem CreateOrderItem() => new PurchaseOrderItem();
        protected override Func<PurchaseOrderItem> InstanceCreator() => CreateOrderItem;
    }







}



