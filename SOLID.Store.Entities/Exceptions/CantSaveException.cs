using System;
using System.Collections.Generic;
using System.Text;

namespace SOLID.Store.Entities.Exceptions{
    public class CantSaveException : Exception {

        public CantSaveException() : this("Can't save this instance since CanSave returned false.") {}
        public CantSaveException(string message) : base(message) { }

        public CantSaveException(BaseEntities.Entity entity) : this($"Cannot save {entity.GetType().Name}") { }
    }

    public class CantDeleteException : Exception {
        public CantDeleteException() : this("Can't delete this instance since CanDelete returned false.") { }
        public CantDeleteException(string message) : base(message) { }
    }

    


}
