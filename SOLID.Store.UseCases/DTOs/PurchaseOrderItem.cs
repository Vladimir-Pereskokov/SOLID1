using System;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.DTOs {
    public class PurchaseOrderItemDTO: pocoBase {
        public PurchaseOrderItemDTO(): base() {}
        internal PurchaseOrderItemDTO(Guid id): base(id) {}
        
        public Guid OrderID {get; set;}
        public decimal Price {get; set;}
        public int Quantity {get; set;}
        public Guid ProductID {get; set;}

    }

}