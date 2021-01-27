namespace OddsSystem.WebClient.Models
{
    using System;

    [Serializable]
    public class EventViewMode
    {
        public EventViewMode()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal OddsForFirstTeam { get; set; }
       
        public decimal OddsForDraw { get; set; }

        public decimal OddsForSecondTeam { get; set; }

        public DateTime EventStartDate { get; set; }

    }
}
