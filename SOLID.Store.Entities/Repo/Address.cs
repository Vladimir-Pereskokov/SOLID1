using System;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.Validation;
using System.Collections.Generic;

namespace SOLID.Store.Entities.Repo
{
    public class Address : EntityBase<AddressDTO>
    {
        protected internal Address()
        {
        }

        protected Address(AddressDTO dto) : base(dto)
        {
        }
        
        protected internal override bool CanRead() => Customer.Current != null;

        public string Address1 {
            get => CanReadProperty(nameof(Address1)) ? DTO.Address1 : default(string);
            set {
                if (!string.Equals(DTO.Address1, value, StringComparison.OrdinalIgnoreCase) &&
                    CanWriteProperty(nameof(Address1))) {
                    DTO.Address1 = value;
                    DTO.IsDirty = true;
                }
            }
        }


        public string Address2
        {
            get => CanReadProperty(nameof(Address2)) ? DTO.City : default(string);
            set
            {
                if (!string.Equals(DTO.Address2, value, StringComparison.OrdinalIgnoreCase) &&
                    CanWriteProperty(nameof(Address2)))
                {
                    DTO.Address2 = value;
                    DTO.IsDirty = true;
                }
            }
        }

        public string City
        {
            get => CanReadProperty(nameof(City)) ? DTO.City : default(string);
            set
            {
                if (!string.Equals(DTO.City, value, StringComparison.OrdinalIgnoreCase) &&
                    CanWriteProperty(nameof(City)))
                {
                    DTO.City = value;
                    DTO.IsDirty = true;
                }
            }
        }


        public string State
        {
            get => CanReadProperty(nameof(State)) ? DTO.State : default(string);
            set
            {
                if (!string.Equals(DTO.State, value, StringComparison.OrdinalIgnoreCase) &&
                    CanWriteProperty(nameof(State)))
                {
                    DTO.State = value;
                    DTO.IsDirty = true;
                }
            }
        }

        public string ZipCode
        {
            get => CanReadProperty(nameof(ZipCode)) ? DTO.ZipCode : default(string);
            set
            {
                if (!string.Equals(DTO.ZipCode, value, StringComparison.OrdinalIgnoreCase) &&
                    CanWriteProperty(nameof(ZipCode)))
                {
                    DTO.ZipCode = value;
                    DTO.IsDirty = true;
                }
            }
        }



        protected override IEnumerable<BrokenRule> OnValidate()
        {

            return null;
        }
    }
}
