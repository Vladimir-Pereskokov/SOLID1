using System;
using System.Collections.Generic;
using SOLID.Store.DTOs.BaseDTO;
using SOLID.Store.Validation;
using SOLID.Store.DataAccess;
using SOLID.Store.Entities.Exceptions;


namespace SOLID.Store.Entities.BaseEntities
{
  /// <summary>
  /// Defines base entity that implements validation and business rules
  /// </summary>    
  public abstract class EntityBase<T> :Entity  where T: pocoBase, new()
    {     
       
        /// <summary>
        /// constructs new entity that has a state IsNew = true
        /// </summary>
        protected EntityBase(): this(new T()) {m_dto.IsNew = true;}

        /// <summary>
        /// Constructs new entity tahs has a state IsDirty = false
        /// </summary>
        /// <param name="dto">DTO object to be used for persistence</param>
        /// <returns></returns>
        protected EntityBase(T dto): base(dto){}

        /// <summary>
        /// Underlying DTO object
        /// </summary>
        /// <returns>Returns reference to the underlying DTO</returns>
        protected internal T DTO => (T)m_dto;
       
        /// <summary>
        /// Marks this instance for deletion
        /// </summary>
        /// <remarks>Call Save method to update a data store</remarks>
        public void Delete(){
            if (!CanDelete()) throw new CantDeleteException();
            m_dto.IsDeleted = true;
        }

        /// <summary>
        /// Saves changes to this instance
        /// </summary>
        public virtual void Save(){
            try {
                IO.IOFacadeService.Current.Update(new Entity[] { this });
            }
            catch {
                throw;
            }
        }       

        
    }
}
