namespace OddsSystem.Core.Entities.Base
{
    using System;

    public interface IDeletableEntity : IEntity
    {
        DateTime? DeletedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}
