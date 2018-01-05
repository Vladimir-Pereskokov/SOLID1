using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID.Store.DataAccess
{
    public class DataAccessException : Exception
    {
        public DataAccessException() : this("Data Access erro occurred") { }
        public DataAccessException(string message) : base(message) { }
    }
}
