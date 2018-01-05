using System;
using System.Linq;
using System.Collections.Generic;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.Entities.IO;
using SOLID.Store.Validation;
using SOLID.Store.ModuleInitializers.Concrete;
using SOLID.Store.DataAccess;


namespace SOLID.Store.Entities.Repo
{
    public class PurchaseOrder : EntityBase<PurchaseOrderDTO>
    {
        internal PurchaseOrder() :base(){}

        internal PurchaseOrder(PurchaseOrderDTO dto) : base(dto){}
                

        public int OrderNumber {
            get => CanReadProperty(nameof(OrderNumber)) ? DTO.OrderNumber: default(int);
            internal set {DTO.OrderNumber = value;} }

        public Guid CustomerID {
            get => CanReadProperty(nameof(CustomerID)) ? DTO.CustomerID : default(Guid);
            set {if (DTO.CustomerID != value && CanWriteProperty(nameof(CustomerID))) {
                DTO.CustomerID = value;
                DTO.IsDirty = true;
                }
            }
        }

        protected override bool CanCreateNew() => Customer.Current != null;       

        public override bool CanEdit() => Customer.Current != null && DTO.CustomerID == Customer.CurrentID;

        public override bool CanDelete() => CanEdit() && DTO.DateProcessed == DateTime.MinValue;


        public override bool CanWriteProperty(string propertyName)
        {
            var result = base.CanWriteProperty(propertyName);
            if (result && string.Compare(propertyName, nameof(CustomerID)) == 0)
                            result = IsNew || (DTO.DateProcessed == DateTime.MinValue);
            return result;
        }


        public DateTime DateProcessed => CanReadProperty(nameof(DateProcessed)) ? DTO.DateProcessed: default(DateTime);

        internal void SetProcessedDate(DateTime value) => DTO.DateProcessed = value;

        private List<PurchaseOrderItem> m_Items = null;
        public List<PurchaseOrderItem> Items { 
            get {if (m_Items == null) {
                    if (!IsNew)
                    {
                        var crit = SearchCriteria.Create<PurchaseOrderItemDTO>();
                        crit[nameof(PurchaseOrderItemDTO.OrderID)] = ID.ToString();
                        m_Items = PurchaseModuleInitializer.Current.FacadeS.ReadEntityList<PurchaseOrderItem>(crit);
                    }
                    else m_Items = new List<PurchaseOrderItem>();                
                }             
            return m_Items;}
            }        
        

        internal void PrepareToProcess() {            
            if(m_Items != null && m_Items.Count > 0) {
                foreach (var member in m_Items) member.Order = this;
            }
        }


        public bool HasMemberDiscount { 
            get {                
                if(Customer.Current != null && Customer.Current.IsMember) return true;
                else  {
                    var members = this.Items;
                    if(members != null && members.Count > 0) {
                        return (members.Any(
                            p => p.productType == ProductType.Membership));                        
                        }
                    }
                    return false;  
            }
        }

        public decimal Total => 
                (m_Items == null || m_Items.Count == 0) ? 0M: m_Items.Sum(p => p.Total);



        public override bool IsDirty {
            get {
                var result = base.IsDirty;
                if (!result && m_Items != null) result = m_Items.Any(item => item.IsDirty);
                return result;
            }
        }

        public override bool IsValid {
            get
            {
                var result = base.IsValid;
                if (result && m_Items != null) result = !m_Items.Any(item => !item.IsValid);
                return result;
            }
        }

        public override bool CanSave() 
            => base.CanSave() && Customer.CurrentID == CustomerID;

        public override void Save()
        {
            if (IsDirty) {                
                List<Entity> lst = null;
                PrepareToProcess();
                if (m_Items != null) {
                    lst = new List<Entity>(from itm in m_Items where itm.IsDirty select itm);
                }
                if (IsDirtySelf || (lst != null && lst.Count > 0)) {
                    if (lst == null) lst = new List<Entity>();
                    lst.Add(this);
                }                
                PurchaseModuleInitializer.Current.FacadeS.Update(lst);
                
                if (m_Items != null) {
                    var deletedItems = from itm in m_Items where itm.IsDeleted select itm;
                    if (deletedItems.Any()) foreach (var delItm in deletedItems) m_Items.Remove(delItm);
                }
            }            
        }
                
        protected override IEnumerable<BrokenRule> OnValidate()
        {
            const string C_NOITEMS = "This purchase order is empty";
            const string C_INVALIDTOTAL = "Purchase Order Total must be greater than 0";
            const string C_INVALIDITEM = "Invalid item";
            const string C_CUSTOMERREQUIRED = "Customer reference is required";
            PrepareToProcess();
            IEnumerable<BrokenRule> result = null;
            var members = this.m_Items;
            if((members == null && IsNew) || (members != null && members.Count ==0)) result = result.PushRule(nameof(Items), C_NOITEMS);
            if (CustomerID == Guid.Empty && Customer.Current != null)
                        CustomerID = Customer.Current.ID;
            if (CustomerID == Guid.Empty) result = result.PushRule(nameof(CustomerID), C_CUSTOMERREQUIRED);
            if (Total<=0) result = result.PushRule(nameof(Total), C_INVALIDTOTAL);
            if (members != null) {            
                foreach (var member in members) {
                    member.Validate();
                    if (!member.IsValid) result = result.PushRule("Item", C_INVALIDITEM);
                }
            }
            return result;
        }

        public bool CanPlaceThisOrder { 
            get {
               if ((DateProcessed == DateTime.MinValue || IsNew) && CustomerID == Customer.CurrentID) {
                Validate();
                return IsValid;
               }
               return false;
            }
        }
    }
}
