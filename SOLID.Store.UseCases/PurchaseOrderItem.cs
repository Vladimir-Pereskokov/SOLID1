using System;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.Validation;
using SOLID.Store.Entities.IO;
using SOLID.Store.ModuleInitializers.Concrete;
using System.Collections.Generic;

namespace SOLID.Store.Entities.Repo
{
    public class PurchaseOrderItem : EntityBase<PurchaseOrderItemDTO>
    {
        internal PurchaseOrderItem() :base(){}

        internal PurchaseOrderItem(PurchaseOrderItemDTO dto) : base(dto){}


        public Guid OrderID {
            get => CanReadProperty(nameof(OrderID)) ? DTO.OrderID: default(Guid);
            set{
                if (DTO.OrderID != value && (m_Order == null || m_Order.ID == value) && CanWriteProperty(nameof(OrderID))) {
                    DTO.OrderID = value;
                    DTO.IsDirty = true;
                }                
            }
        }

        

        public int Quantity{
            get => CanReadProperty(nameof(Quantity)) ? DTO.Quantity: default(int);
            set {if (DTO.Quantity != value && CanWriteProperty(nameof(Quantity))) {
                DTO.Quantity = value;
                DTO.IsDirty = true;
                }
            }
        }

        public Guid ProductID {
            get => CanReadProperty(nameof(ProductID)) ? DTO.ProductID: default(Guid);
            set {if (DTO.ProductID != value && CanWriteProperty(nameof(ProductID))) {
                    DTO.ProductID = value;
                    DTO.IsDirty = true;
                }                
            }
        }

        private ProductDTO m_Product = null;
        internal ProductDTO Product {
            get {if (m_Product == null && ProductID != Guid.Empty) 
                m_Product =  PurchaseModuleInitializer.Current.DAccessSvc.GetDto<ProductDTO>(ProductID);
             return m_Product;
            } 
            set {m_Product = value ; 
            ProductID = (value == null)? Guid.Empty: value.ID;}
        }

        public ProductType productType { 
            get {
                var prod = this.Product;
                return (prod != null) ? (ProductType)prod.Type: ProductType.Book; 
            }
        }

        internal bool IsPhysicalProduct => Repo.Product.IsPhysicalProductType(productType);

        private PurchaseOrder m_Order = null;
        internal PurchaseOrder Order {
            get { if (m_Order == null && OrderID != Guid.Empty)
                    m_Order = PurchaseModuleInitializer.Current.FacadeS.ReadEntity<PurchaseOrder>(OrderID);
                    return m_Order;
            } 
            set
            { m_Order = value;
            OrderID = (value == null)?  Guid.Empty: value.ID;
            }
        } 

        private bool HasMemberDiscount { get { 
                if (productType == ProductType.Membership) return false;
                else {
                    var ord = this.Order;
                    return (ord==null)? false: ord.HasMemberDiscount;
                    }
                }
        }

        public decimal Price{
            get {return (m_Product != null)? m_Product.FullPrice: DTO.Price;} 
            internal set {DTO.Price = value;}}

        public decimal EffectivePrice => HasMemberDiscount? (decimal)((double)Price * 0.9): Price; 
        public decimal Total => EffectivePrice * Quantity;


        protected override IEnumerable<BrokenRule> OnValidate()
        {
            const string C_PRICE = "Price must be greater than 0";
            const string C_ORDER = "Order Reference is required";
            const string C_PROD = "Product Reference is required";
            const string C_QTY = "Quantity must be greater than 0";
            IEnumerable<BrokenRule> result = null;
            if (OrderID == Guid.Empty) result = result.PushRule(nameof(OrderID), C_ORDER);            
            if (Price<=0M) result = result.PushRule(nameof(Price), C_PRICE);
            if (ProductID == Guid.Empty) result = result.PushRule(nameof(ProductID), C_PROD);
            if (Quantity <= 0M) result =  result.PushRule(nameof(Quantity), C_QTY);
            return result;
        }

        protected override bool CanCreateNew() => Customer.Current != null;
        public override bool CanEdit() => (Customer.Current != null) && (m_Order != null) && m_Order.CustomerID == Customer.CurrentID;
        public override bool CanDelete() => CanEdit() && m_Order.CanDelete();
        protected override bool CanRead() => CanCreateNew();

    }
}