using System;
using System.Collections.Generic;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.Repo;

namespace SOLID.Store.UseCases
{
    /// <summary>
    /// Must be implemented by a MVC controller or a MVP presenter to be used by <see cref="PurchaseOrderProcessor"/>
    /// </summary>
    public interface IPurchaseOrderDataProvider
    {
        /// <summary>
        /// Purchase order POCO object to be used to construct the <see cref="PurchaseOrder"/> business object
        /// </summary>
        PurchaseOrderDTO Order { get; }

        /// <summary>
        /// list of purchase order item objects to be used to construct the <see cref="PurchaseOrder"/> business object
        /// </summary>
        IEnumerable<PurchaseOrderItemDTO> Items { get; }
    }
}
