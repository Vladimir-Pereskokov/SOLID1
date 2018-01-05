
using System;
using System.Linq;
using System.Collections.Generic;
using SOLID.Store.DTOs.BaseDTO;
using SOLID.Store.Validation;
using SOLID.Store.DataAccess;


namespace SOLID.Store.Entities.BaseEntities
{
    public abstract class Entity: IMayHaveBrokenRules {
        protected internal pocoBase m_dto = null;       

        
        /// <summary>
        /// Constructs new Entity instance based on given dto object
        /// </summary>        
        protected Entity(pocoBase dto) : base() {m_dto = dto;}

        
        /// <summary>
        /// Checks if given propery value can be read.
        /// </summary>
        /// <param name="propertyName">name of the property to check</param>
        /// <returns>Returns true in case given property value can be read.</returns>
        public virtual bool CanReadProperty(string propertyName) => CanRead();
        
        /// <summary>
        /// Checks if given propery value can be written.
        /// </summary>
        /// <param name="propertyName">name of the property to check</param>
        /// <returns>Returns true in case given property value can be written.</returns>
        public virtual bool CanWriteProperty(string propertyName) => CanEdit();


        /// <summary>
        /// Checkes if a new instance of this type can be created
        /// </summary>
        /// <returns>Returns true in case new instance can be created</returns>
        protected internal virtual bool CanCreateNew() => false;


        /// <summary>
        /// Checks if records of this type can be read
        /// </summary>
        /// <returns></returns>
        protected internal virtual bool CanRead() => true;



        /// <summary>
        /// Checks if this instance can be deleted
        /// </summary>
        /// <returns></returns>
        public virtual bool CanDelete() => false;

        /// <summary>
        /// Checks if this instance can be saved
        /// </summary>
        /// <returns></returns>
        public virtual bool CanSave() => IsValid && IsDirty;

        /// <summary>
        /// Checks if this instance can be edited
        /// </summary>
        /// <returns></returns>
        public virtual bool CanEdit() => IsNew && CanCreateNew();
       

        /// <summary>
        /// Returns entity ID
        /// </summary>
        public Guid ID => m_dto.ID;

        private IEnumerable<BrokenRule> m_brokenRules = null;

        /// <summary>
        /// Triggers entity validation 
        /// </summary>
        public void Validate() {m_brokenRules = OnValidate();}

        /// <summary>
        /// Must implement entity-specific validation logic
        /// </summary>
        /// <returns>List of broken rule descriptions or null if entity is valid</returns>
        protected abstract IEnumerable<BrokenRule> OnValidate(); 

        /// <summary>
        /// Use this method to retrieve list of broken rule descriptions.
        /// </summary>
        /// <returns>List of broken rule descriptions or null if entity is valid</returns>
        /// <remarks>Call <ref>Validate</ref> first to trigger the validation mechanism</remarks>
        public IEnumerable<BrokenRule> GetBrokenRules() => m_brokenRules;

        /// <summary>
        /// Returns true in case this entity is valid or fasle otherwise.
        /// </summary>
        public virtual bool IsValid => m_brokenRules == null || !m_brokenRules.Any();

        protected internal bool IsValidSelf => IsValidSelf;
        /// <summary>
        /// Returns true in case this entity has been changed, deleted, or created but the changes have not been saved yet
        /// </summary>
        public virtual bool IsDirty => IsDirtySelf;

        protected internal bool IsDirtySelf => m_dto.IsDirty;

        /// <summary>
        /// Returns true in case this entity has been deleted
        /// </summary>
        public bool IsDeleted => m_dto.IsDeleted;

        /// <summary>
        /// Returns true in case this entity has been created but not yet saved
        /// </summary>
        public bool IsNew => m_dto.IsNew;
        IEnumerable<BrokenRule> IMayHaveBrokenRules.GetBrokenRules() => GetBrokenRules();

    }

}