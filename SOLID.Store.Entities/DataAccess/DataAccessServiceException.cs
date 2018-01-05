using System;

namespace SOLID.Store.DataAccess
{
    public class NoDataAccessorException : DataAccessException
    {
        internal NoDataAccessorException(Type t): 
        base($"Data Accessor for type {t.Name} does not exist") {}
    }
}