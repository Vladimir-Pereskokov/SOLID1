using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID.Store.Authentication
{
    public class AuthenticationException: Exception
    {
        public AuthenticationException() : this("This operation requires customer login") { }
        public AuthenticationException(string message) : base(message) { }
    }
}
