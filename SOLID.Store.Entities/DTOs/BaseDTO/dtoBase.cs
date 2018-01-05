using System;

namespace SOLID.Store.DTOs.BaseDTO {
    public abstract class pocoBase {
        private bool m_isDirty = false;

        protected pocoBase() : this(Guid.NewGuid()) {IsNew = true;}

        protected pocoBase(Guid id) {this.ID = id;}
               

        public Guid ID {get; set;}

        public bool IsNew {get; set;}     
        public bool IsDeleted {get; set;}

        public bool IsDirty{get {return m_isDirty || IsNew || IsDeleted;}
         set {m_isDirty = value;}}
       
        public void MarkClean() {IsDirty = false; IsNew = false; IsDeleted = false;}
    }
}


