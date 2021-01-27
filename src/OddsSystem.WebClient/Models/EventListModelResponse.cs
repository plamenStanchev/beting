namespace OddsSystem.WebClient.Models
{
    using System.Collections.Generic;

    public class EventListModelResponse
    {
        public IEnumerable<EventViewMode> Events { get; set; }

        public bool Succeeded { get; set; }

        public string ErrorMesage { get; set; }
    }
}
