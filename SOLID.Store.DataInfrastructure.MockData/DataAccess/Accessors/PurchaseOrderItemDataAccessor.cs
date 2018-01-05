using System;
using System.Linq;
using SOLID.Store.DTOs;
using SOLID.Store.DataInfrastructure.MockData.DataAccess;
using SOLID.Store.DataAccess;
using SOLID.Store.DTOs.BaseDTO;
using System.Collections.Generic;

namespace SOLID.Store.DataInfrastructure.MockData.DataAccess.Accessors
{
    internal class PurchaseOrderItemDataAccessor : MockDataAccessor<PurchaseOrderItemDTO> {
        public override IEnumerable<pocoBase> Read(SearchCriteria criteria)
        {
            if (Guid.TryParse(criteria[nameof(PurchaseOrderItemDTO)], out var orderid)) {                
                return  (from r in Records where r.OrderID == orderid select r);
            }
            else return base.Read(criteria);
        }
    }
}
