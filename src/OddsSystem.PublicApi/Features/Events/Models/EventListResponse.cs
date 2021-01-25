namespace OddsSystem.PublicApi.Features.Events.Models
{
    using System.Collections.Generic;
    using OddsSystem.Core.Entities.Models;
    using OddsSystem.PublicApi.Features.BaseFeatures.Models;

    public class EventListResponse : BaseResponseModel
    {
        public IEnumerable<EventViewMode> Events { get; set; }

    }
}
