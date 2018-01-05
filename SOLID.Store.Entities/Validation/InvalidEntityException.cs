using System;
using SOLID.Store.Entities.BaseEntities;


namespace SOLID.Store.Validation
{
    /// <summary>
    /// Is thrown when an invalid enity is attempted to be saved.
    /// </summary>
    public class InvalidEntityException : Exception { 
        /// <summary>
        /// Constructs invalid entity exception for a given message
        /// </summary>
        /// <param name="message">Invalid enity message</param>
        public InvalidEntityException(string message): base(message) {}

        /// <summary>
        /// Constructs invalid entity exception
        /// </summary>
        public InvalidEntityException(): this("Invalid entity") {} 

        /// <summary>
        /// Constructs invalid entity exception for a given entity
        /// </summary>
        /// <param name="entity"></param>
        public InvalidEntityException(IMayHaveBrokenRules entity): this(entity.Summary()) {}        
    }
}