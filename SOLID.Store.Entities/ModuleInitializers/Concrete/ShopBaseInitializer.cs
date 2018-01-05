using System;
using System.Collections.Generic;
using System.Text;
using SOLID.Store.ModuleInitializers;
using SOLID.Store.DataAccess;


namespace SOLID.Store.ModuleInitializers.Concrete
{
    public class ShopBaseInitializer : Initializer
    {
        public ShopBaseInitializer(IDataAccessor customerAccessor) : 
            this(new EntityInitializer[] { new CustomerInitializer(customerAccessor) }) { }
        public ShopBaseInitializer(IEnumerable<EntityInitializer> initializers) : base(initializers) { }
        
    }
}
