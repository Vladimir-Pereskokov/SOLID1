using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SOLID.Store.DTOs;


namespace SOLID.Store.DataInfrastructure.MockData
{
    public static class MockAddresses
    {
        public const string MOCK_ADDRESS_ID = "25BF86AB-CC57-494A-97AB-9A729B1B4389";

        private static AddressDTO m_MockAddress;
        public static AddressDTO GetMockAddress() {
            if (m_MockAddress == null) {
                m_MockAddress = new AddressDTO(new Guid(MOCK_ADDRESS_ID))
                { Address1 = "1600 Pennsylvania Ave NW", City = "Washington", State = "DC", ZipCode = "20500"};
            }
            return m_MockAddress;
        }

    }
}
