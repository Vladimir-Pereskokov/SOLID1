using System;
using Xunit;
using SOLID.Store.Entities.Repo;
using SOLID.Store.DataInfrastructure.MockData;
using SOLID.Store.DataInfrastructure.MockData.StartUpConfig;
using SOLID.Store.Entities.IO;
using SOLID.Store.DTOs;
using SOLID.Store.DataAccess;
using System.Collections.Generic;

namespace SOLID.Store.Entities.Test
{
    public class AddressTest
    {
        [Fact]
        public void Can_Read_Authenticated()
        {
            using (var cfg = new InfrastructureConfig(true)) {
                var addresses = IOFacadeService.Current.ReadEntityList<Address>(SearchCriteria.Create<AddressDTO>());
                Assert.NotEmpty(addresses);
                Assert.True(addresses.Count == 1);
                Assert.True(addresses[0].ID == new Guid(MockAddresses.MOCK_ADDRESS_ID));
            }
        }
        [Fact]
        public void Cannot_Browse()
        {
            using (var cfg = new InfrastructureConfig(false))
            {
                List<Address> addresses = null;
                Assert.Throws<SOLID.Store.Authorization.AuthorizationException>( 
                    () =>
                    addresses = IOFacadeService.Current.ReadEntityList<Address>(
                        SearchCriteria.Create<AddressDTO>())
                );               
            }
        }
    }
}
