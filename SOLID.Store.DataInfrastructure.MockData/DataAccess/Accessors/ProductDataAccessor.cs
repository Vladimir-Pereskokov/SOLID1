using System;
using SOLID.Store.DTOs;
using SOLID.Store.DataInfrastructure.MockData.DataAccess;

namespace SOLID.Store.DataInfrastructure.MockData.DataAccess.Accessors
{
    internal class ProductDataAccessor : MockDataAccessor<ProductDTO> {
        protected override void OnInitRecords() => Records.AddRange(MockProducts.GetProducts());
    }
}
