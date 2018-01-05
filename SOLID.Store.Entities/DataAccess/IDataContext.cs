using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID.Store.DataAccess
{
    public interface IDataContext
    {
        /// <summary>
        /// Begins data transaction
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits data transaction
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// Saves data changes
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Rolls back data transaction
        /// </summary>
        void RollBackTransaction();
    }
}
