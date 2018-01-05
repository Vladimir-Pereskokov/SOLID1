using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID.Store.Authorization
{
    public class AuthorizationException: Exception
    {
        public AuthorizationException() : this("Current user is not authorized to perform this operation") { }
        public AuthorizationException(string message) : base(message) { }
    }
}
