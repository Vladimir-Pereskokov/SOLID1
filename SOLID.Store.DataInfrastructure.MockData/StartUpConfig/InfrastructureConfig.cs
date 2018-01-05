using System;
using System.Collections.Generic;
using SOLID.Store.DataAccess;
using SOLID.Store.DataInfrastructure.MockData.DataAccess;
using SOLID.Store.ModuleInitializers;
using SOLID.Store.ModuleInitializers.Concrete;
using SOLID.Store.Authentication;
using SOLID.Store.DataInfrastructure.MockData.DataAccess.Accessors;


namespace SOLID.Store.DataInfrastructure.MockData.StartUpConfig
{
    public class InfrastructureConfig: IDisposable
    {
        public InfrastructureConfig():this(false) {}

        public InfrastructureConfig(bool loginMockCustomer):base() {
            DataContext.Current.ClearAccessors();
            Initializer.Init(new Initializer[] {
              new ShopBaseInitializer(new EntityInitializer[] 
                {
                    new CustomerInitializer(new CustomerDataAccessor()),
                    new AddressInitializer(new AddressDataAccessor())
                }),
              new PurchaseModuleInitializer(new EntityInitializer[]
                {
                    new ProductEntityInitializer(new ProductDataAccessor()),
                    new PurchaseOrderEntityInitializer(new PurchaseOrderDataAccessor()),
                    new PurchaseOrderItemEntityInitializer(new PurchaseOrderItemDataAccessor()),
                    new ShippingSlipEntityInitializer(new ShippingSlipDataAccessor())
                })
            }
            );


            if (loginMockCustomer) CustomerBasedPrincipal.Login(new Guid(MockCustomers.MOCK_CUSTOMER_ID));
            

        }

        public void Dispose() {
            DataContext.Current.ClearAccessors();
            CustomerBasedPrincipal.Logout(); }

    }
}
