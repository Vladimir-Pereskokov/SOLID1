using System;
using System.Collections.Generic;
using SOLID.Store.DTOs.BaseDTO;
using SOLID.Store.Entities;


namespace SOLID.Store.DataAccess {


    public interface IDataAccessor {

        /// <summary>
        /// Returns the type of dto object this accessor is defined for
        /// </summary>
        Type dtoType { get; }

        IEnumerable<pocoBase> Read(SearchCriteria criteria);

        /// <summary>
        /// Runs update operations for given items
        /// </summary>
        /// <param name="items"></param>
        void Update(IEnumerable<pocoBase> items);

        /// <summary>
        /// Runs Create operations for given items
        /// </summary>
        /// <param name="items"></param>
        void Create(IEnumerable<pocoBase> items);

        /// <summary>
        /// Runs Delete operations for given items
        /// </summary>
        /// <param name="items"></param>
        void Delete(IEnumerable<pocoBase> items);

        /// <summary>
        /// Returns reference to the parent data context and its transactions
        /// </summary>
        IDataContext Context { get; }
    }

}

