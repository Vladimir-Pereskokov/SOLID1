﻿using System;
using SOLID.Store.DTOs;
using SOLID.Store.DataInfrastructure.MockData.DataAccess;

namespace SOLID.Store.DataInfrastructure.MockData.DataAccess.Accessors
{
    internal class AddressDataAccessor: MockDataAccessor<AddressDTO> {
        protected override void OnInitRecords()=> Records.Add(MockAddresses.GetMockAddress());
    }
}
