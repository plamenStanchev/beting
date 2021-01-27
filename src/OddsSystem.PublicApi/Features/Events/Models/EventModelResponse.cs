namespace OddsSystem.PublicApi.Features.Events.Models
{
    using System;
    using OddsSystem.PublicApi.Features.BaseFeatures.Models;

    public class EventModelResponse : BaseResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal OddsForFirstTeam { get; set; }

        public decimal OddsForDraw { get; set; }

        public decimal OddsForSecondTeam { get; set; }

        public DateTime EventStartDate { get; set; }
    }
}
