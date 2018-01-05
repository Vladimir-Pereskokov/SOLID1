using System;
using System.Collections.Generic;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.Validation;


namespace SOLID.Store.Entities.Repo
{
    public class Product : EntityBase<ProductDTO>
    {
        internal Product() :base(){}

        internal Product(ProductDTO dto) : base(dto){}  

        public string Name {
            get => CanReadProperty(nameof(Name)) ? DTO.Name : default(string);
            set {if (string.Compare(DTO.Name, value) != 0 && CanWriteProperty(nameof(Name))) 
                    {
                        DTO.Name = value;
                        DTO.IsDirty = true;
                    }
            }      
        }

        public decimal FullPrice {
            get => CanReadProperty(nameof(FullPrice)) ? DTO.FullPrice : default(Decimal);
            set { if (DTO.FullPrice != value && CanWriteProperty(nameof(FullPrice))) {
                    DTO.FullPrice = value;
                    DTO.IsDirty = true;
                    }                
                }
        }

        public ProductType Type {
            get => (ProductType)DTO.Type;
            set {
                if (DTO.Type != (int)value && CanWriteProperty(nameof(Type))) {
                    DTO.Type = (int)value;
                    DTO.IsDirty = true;
                }
            }
                
        }


        /// <summary>
        /// Returns true for physical products that require shipping slips
        /// </summary>
        public bool IsPhysicalProduct => IsPhysicalProductType(Type);


        internal static bool IsPhysicalProductType(ProductType type) => type == ProductType.Book;

        protected override IEnumerable<BrokenRule>  OnValidate()
        {
            const string C = "Full Price must be greater than 0";
            IEnumerable<BrokenRule> result = null;
            if (string.IsNullOrEmpty(Name)) result = result.PushRule(nameof(Name), "Product name is required");
            if (FullPrice<=0M) result = result.PushRule(nameof(FullPrice), C);                                                        
            return result;
        }
        
    }
}