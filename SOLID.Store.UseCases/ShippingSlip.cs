using System;
using System.Collections.Generic;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.Validation;


namespace SOLID.Store.Entities.Repo
{
    public class ShippingSlip : EntityBase<ShippingSlipDTO>
    {
        internal ShippingSlip()
        {
        }

        internal ShippingSlip(ShippingSlipDTO dto) : base(dto)
        {
        }


        protected override bool CanCreateNew() => Customer.Current != null;
        public override bool CanEdit() => CanCreateNew();


        public Guid ShippingAddressID {
            get => CanReadProperty(nameof(ShippingAddressID)) ? DTO.ShippingAddressID : default(Guid);
            set {
                if (DTO.ShippingAddressID != value && CanWriteProperty(nameof(ShippingAddressID))) {
                    DTO.ShippingAddressID = value;
                    DTO.IsDirty = true;
                }
            }
        }

        public Guid OrderItemID
        {
            get => CanReadProperty(nameof(OrderItemID)) ? DTO.OrderItemID : default(Guid);
            set
            {
                if (DTO.OrderItemID != value && CanWriteProperty(nameof(OrderItemID)))
                {
                    DTO.OrderItemID = value;
                    DTO.IsDirty = true;
                }
            }
        }

        


        protected override IEnumerable<BrokenRule> OnValidate()
        {
            IEnumerable<BrokenRule> result = null;
            if (DTO.ShippingAddressID == Guid.Empty)
                result = result.PushRule(nameof(ShippingAddressID), "Address reference is required");
            if (DTO.OrderItemID == Guid.Empty)
                result = result.PushRule(nameof(OrderItemID), "Order item reference is required");

            return result;
        }
    }
}
