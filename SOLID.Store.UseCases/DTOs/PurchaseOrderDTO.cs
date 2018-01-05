using System;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.DTOs {
    public class PurchaseOrderDTO: pocoBase {
        public PurchaseOrderDTO(): base() {}
        public PurchaseOrderDTO(Guid id): base(id) {}

        public int OrderNumber {get; set;}
        public Guid CustomerID {get; set;}

        public DateTime DateProcessed {get; set;}

    }

}