using System;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.DTOs {
    public class CustomerDTO :pocoBase {
        public CustomerDTO(): base() {}
        public CustomerDTO(Guid id): base(id) {}
        public string Name { get; set;}
        public bool IsMember {get; set;}
        public Guid ShippingAddressID { get; set; }
    }
}