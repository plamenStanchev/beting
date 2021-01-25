namespace OddsSystem.Core.Entities.Base
{
    using System;

    public interface IEntity
    {
        DateTime CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }

    }
}
