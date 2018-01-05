using System;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.Validation;
using System.Collections.Generic;

namespace SOLID.Store.Entities.Repo
{
    public class Customer : EntityBase<CustomerDTO>
    {
        protected internal Customer() :base(){}

        private static Customer m_Current = null;
        public static Customer Current => m_Current;
        public static Guid CurrentID => m_Current != null ? m_Current.ID: default(Guid);

        internal static void SetCustomer(Customer value) => m_Current = value;
        internal static void SetCustomer(CustomerDTO value) => m_Current = new Customer(value);

        protected Customer(CustomerDTO dto) : base(dto){}

        public string Name => CanReadProperty(nameof(Name))? DTO.Name: null;

        public Guid ShippingAddressID => CanReadProperty(nameof(ShippingAddressID)) ? DTO.ShippingAddressID : default(Guid);

        private Address m_ShippingAddress;
        public Address ShippingAddress {
            get {
                if (m_ShippingAddress == null && DTO.ShippingAddressID != Guid.Empty) {
                    m_ShippingAddress = IO.IOFacadeService.Current.ReadEntity<Address>(DTO.ShippingAddressID);
                }
                return m_ShippingAddress;
            }
        }

        public bool IsMember => DTO.IsMember;
        protected override IEnumerable<BrokenRule> OnValidate() => 
            string.IsNullOrEmpty(Name)? 
                BrokenRulesProcessor.PushRule(null, new BrokenRule(nameof(Name), 
                "Customer name is required")): null;
    }
}