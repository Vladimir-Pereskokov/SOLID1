using System;
using System.Linq;
using SOLID.Store.DTOs;
using SOLID.Store.Entities.BaseEntities;
using SOLID.Store.Entities.Repo;
using SOLID.Store.Entities.IO;
using System.Collections.Generic;

namespace SOLID.Store.UseCases {
    /// <summary>
    /// Processes purchase order
    /// </summary>
    public class PurchaseOrderProcessor
    {
        private readonly IPurchaseOrderDataProvider m_provider;

        /// <summary>
        /// Constructs new instance of the purchase order processor
        /// </summary>
        /// <param name="provider">required POCO objects provider</param>
        public PurchaseOrderProcessor(IPurchaseOrderDataProvider provider) { m_provider = provider;}        

        /// <summary>
        /// Places the purchase order
        /// </summary>
        /// <returns>If successful then returns saved <see cref="PurchaseOrder"/> object</returns>
        public PurchaseOrder PlaceOrder() {
            if (Customer.Current == null) throw new Authentication.AuthenticationException();
            if (m_provider == null)
                throw new ArgumentException("provider argument is empty");
            if (m_provider.Order == null)
                throw new ArgumentException("provider must return Order POCO object");
            if (m_provider.Items  == null || !m_provider.Items.Any())
                throw new ArgumentException("provider must return at least one item POCO object");
            var objOrder = IOFacadeService.Current.CreateNew<PurchaseOrder>(m_provider.Order);
            
            foreach (var itm in m_provider.Items)
                objOrder.Items.Add(IOFacadeService.Current.CreateNew<PurchaseOrderItem>(itm));
            
            objOrder.PrepareToProcess();
            objOrder.Validate();
            if (!objOrder.IsValid) throw new Validation.InvalidEntityException(objOrder);
            
            var lstUpdateItems = new List<Entity>(objOrder.Items);
            lstUpdateItems.Add(objOrder);
            //check if need to create shipping slips
            foreach (var item in objOrder.Items) {
                if (item.IsPhysicalProduct) {
                    var slipDTO = new ShippingSlipDTO()
                    {
                        OrderItemID = item.ID,
                        ShippingAddressID = Customer.Current.ShippingAddressID
                    };
                    lstUpdateItems.Add(IOFacadeService.Current.CreateNew<ShippingSlip>(slipDTO));
                } 
            }
            objOrder.SetProcessedDate(DateTime.UtcNow); 
            IOFacadeService.Current.Update(lstUpdateItems);
            return objOrder;
        }


    }

}