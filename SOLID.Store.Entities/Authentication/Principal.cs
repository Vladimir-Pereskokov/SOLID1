using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text;
using SOLID.Store.Entities.Repo;
using SOLID.Store.DataAccess;
using SOLID.Store.DTOs;
using System.Threading;
using System.Net.Http;

namespace SOLID.Store.Authentication
{
    public abstract class Principal : IPrincipal
    {
        private static Principal m_Current = new UnauthenticatedPrincipal();
        private readonly Identity m_Identity;
        internal Principal(Identity identity) : base() { m_Identity = identity; }

        public static Principal Current => m_Current;

        
        internal static void SetCurrent(Principal value) {
            if (value == null) value = new UnauthenticatedPrincipal();
            m_Current = value;
            Thread.CurrentPrincipal = value;
        }

        public IIdentity Identity => m_Identity;
        public abstract bool IsInRole(string role);
    }

    internal class UnauthenticatedPrincipal : Principal
    {        
        internal UnauthenticatedPrincipal() : base(new UnauthenticatedIdentity()) {}
        public override bool IsInRole(string role) => string.Compare(role, "guest", true) == 0;
    }


    public class CustomerBasedPrincipal : Principal
    {
        private CustomerBasedPrincipal() : base(new CustomerBasedIdentity()) { }
        public override bool IsInRole(string role) => string.Compare(role, "customers", true) == 0;

        public static void Login(Guid customerID) {
            try
            {
                var dtoCust = DataAccessService.Current.GetDto<CustomerDTO>(customerID);
                if (dtoCust == null) throw new AuthenticationException("Invalid customer ID");
                Customer.SetCustomer(dtoCust);
                Principal.SetCurrent(new CustomerBasedPrincipal());
            }
            catch { throw;}            
        }
        public static void Logout() {
            if (Principal.Current != null && Principal.Current is CustomerBasedPrincipal) Principal.SetCurrent(null);
        }

        private class CustomerBasedIdentity : Identity {
            internal CustomerBasedIdentity() : base() { }

            public override string AuthenticationType => "Shop";

            public override bool IsAuthenticated => true;

            public override string Name => Customer.Current == null ? "unknown": Customer.Current.Name;
        }
    }



}
