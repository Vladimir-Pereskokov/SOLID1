using System;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.DTOs
{
    public class ShippingSlipDTO: pocoBase
    {

        public ShippingSlipDTO() : base() { }
        internal ShippingSlipDTO(Guid id) : base(id) { }
        public Guid OrderItemID { get; set; }
        public Guid ShippingAddressID { get; set; }
        public DateTime ShipDate { get; set; }

    }
}
