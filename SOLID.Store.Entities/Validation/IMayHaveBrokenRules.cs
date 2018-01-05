using System;
using System.Collections.Generic;

namespace SOLID.Store.Validation
{
    /// <summary>
    /// Defines interface for an entity that may have broken business rules associated with it
    /// </summary>
    public interface IMayHaveBrokenRules {
        /// <summary>
        /// Allows enumerating all broken rules
        /// </summary>
        /// <returns></returns>
        IEnumerable<BrokenRule> GetBrokenRules();       

    }
}