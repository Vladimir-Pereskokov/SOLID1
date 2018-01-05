using System;
using SOLID.Store.DTOs.BaseDTO;

namespace SOLID.Store.DTOs {
    public class ProductDTO :pocoBase {
        public ProductDTO(): base() {}
        public ProductDTO(Guid id): base(id) {}
        public string Name { get; set;}
        public int Type {get; set;}
        public decimal FullPrice{get; set;}
    }
}