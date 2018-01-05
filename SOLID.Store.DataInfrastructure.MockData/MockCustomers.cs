using System;
using System.Collections.ObjectModel;
using SOLID.Store.DTOs;


namespace SOLID.Store.DataInfrastructure.MockData
{
    public static class MockCustomers
    {
        public const string MOCK_CUSTOMER_ID = "5C31C96C-2EF4-4296-9B07-C6B39C01AA4C";
        public const string MOCK_CUSTOMER_NAME = "Robert Martin";

        private static ReadOnlyCollection<CustomerDTO> m_Customers;

        public static ReadOnlyCollection<CustomerDTO> GetCustomers() {
            if (m_Customers == null) {
                m_Customers = new ReadOnlyCollection<CustomerDTO>(
                    new CustomerDTO[] {
                        new CustomerDTO(new Guid(MOCK_CUSTOMER_ID))
                        { Name = MOCK_CUSTOMER_NAME,
                          ShippingAddressID = new Guid(MockAddresses.MOCK_ADDRESS_ID)
                        }
                    });                
            }
            return m_Customers;
        }

    }

}
