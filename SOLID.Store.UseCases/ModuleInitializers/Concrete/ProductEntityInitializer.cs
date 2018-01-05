using System;
using System.Collections.Generic;
using SOLID.Store.Entities.Repo;
using SOLID.Store.DataAccess;
using SOLID.Store.Entities.IO;

namespace SOLID.Store.ModuleInitializers.Concrete
{
    public class ProductEntityInitializer:EntityInitializer<Product>
    {
        public ProductEntityInitializer(DataAccess.IDataAccessor productAccessor) : base(productAccessor) { }
        protected override Func<Product> InstanceCreator() => CreateProduct;
        private static Product CreateProduct() => new Product();
    }
}
