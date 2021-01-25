namespace OddsSystem.Core.Entities.Models
{
    using System;
    using OddsSystem.Core.Entities.Base;

    public class Event : DeletableEntity
    {
        // ToDo data validation
        public Event(
            string name,
            decimal oddsForFirstTeam,
            decimal oddsForDraw,
            decimal oddsForSecondTeam,
            DateTime eventStartDate)
        {
            this.Name = name;
            this.OddsForFirstTeam = oddsForFirstTeam;
            this.OddsForDraw = oddsForDraw;
            this.OddsForSecondTeam = oddsForSecondTeam;
            this.EventStartDate = eventStartDate;
        }

        public Event()
        {
            // required by EF
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public decimal OddsForFirstTeam { get; private set; }

        public decimal OddsForDraw { get; private set; }

        public decimal OddsForSecondTeam { get; private set; }

        public DateTime EventStartDate { get; private set; }

        public void UpdateDetails(
            string name,
            decimal oddsForFirstTeam,
            decimal oddsForDraw,
            decimal oddsForSecondTeam,
            DateTime eventStartDate)
        {
            this.Name = name;
            this.OddsForFirstTeam = oddsForFirstTeam;
            this.OddsForDraw = oddsForDraw;
            this.OddsForSecondTeam = oddsForSecondTeam;
            this.EventStartDate = eventStartDate;
        }
    }
}
