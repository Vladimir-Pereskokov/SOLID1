using System;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.DTOs
{
    public class AddressDTO: pocoBase
    {
        public AddressDTO() : base() { }
        public AddressDTO(Guid id) : base(id) { }
        public string Address1 { get; set; }
        public string Address2 { get; set; }        
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
