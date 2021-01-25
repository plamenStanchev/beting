namespace OddsSystem.Core.Entities.Base
{
    using System;

    public abstract class DeletableEntity : Entity, IDeletableEntity
    {
        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}
