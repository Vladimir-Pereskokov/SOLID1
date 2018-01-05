using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Principal;

namespace SOLID.Store.Authentication
{
    public abstract class Identity : IIdentity
    {        
        public abstract string AuthenticationType { get; } 

        public abstract bool IsAuthenticated { get; }

        public abstract string Name { get;}
    }

    internal class UnauthenticatedIdentity : Identity
    {
        internal UnauthenticatedIdentity() : base() {} 
        public override string AuthenticationType => "unknow";
        public override bool IsAuthenticated => false;
        public override string Name => "Unauthenticated";
    }
}
