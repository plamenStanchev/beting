namespace OddsSystem.PublicApi.Features.Events
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using OddsSystem.Core.Entities.Models;
    using OddsSystem.Core.Interfaces.Repository;
    using OddsSystem.PublicApi.Features.BaseFeatures.Controllers;
    using OddsSystem.PublicApi.Features.Events.Models;

    public class EventController : ApiController
    {
        private readonly IDeletableEntityRepository<Event> eventRepository;

        public EventController(IDeletableEntityRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        [HttpGet]
        [Route(nameof(List))]
        public async Task<ActionResult<EventListResponse>> List(
            CancellationToken cancellationToken = default)
            => this.Ok(await this.AggregateResponseData(cancellationToken));

        [HttpPost]
        [Route(nameof(Create))]

        public async Task<ActionResult<EventListResponse>> Create(
            EventViewMode eventViewMode,
            CancellationToken cancellationToken = default)
        {
            var Event = new Event(
                eventViewMode.Name,
                eventViewMode.OddsForFirstTeam,
                eventViewMode.OddsForFirstTeam,
                eventViewMode.OddsForSecondTeam,
                eventViewMode.EventStartDate);
            await this.eventRepository.AddAsync(Event);
            var eventResponse = new EventListResponse();
            var result = await this.eventRepository.SaveChangesAsync();
            if (result == 0)
            {
                eventResponse.ErrorMesage = "Ther was an Error";
                eventResponse.Succeeded = false;
            }
            else
            {
                eventResponse = await this.AggregateResponseData(cancellationToken);
            }

            return eventResponse;
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<ActionResult<EventListResponse>> Delete(
            int id,
            CancellationToken cancellationToken = default)
        {
            var existingEvent = await this.eventRepository.All()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            this.eventRepository.Delete(existingEvent);
            await this.eventRepository.SaveChangesAsync(cancellationToken);
            return this.Ok(await this.AggregateResponseData(cancellationToken));
        }

        [HttpPatch]
        [Route(nameof(Update))]
        public async Task<ActionResult<EventListResponse>> Update(
            EventViewMode eventViewMode,
            CancellationToken cancellationToken = default)
        {
            var existingEvent = await this.eventRepository.All()
                .FirstOrDefaultAsync(e => e.Id == eventViewMode.Id, cancellationToken);

            existingEvent.UpdateDetails(
                eventViewMode.Name,
                eventViewMode.OddsForFirstTeam,
                eventViewMode.OddsForDraw,
                eventViewMode.OddsForSecondTeam,
                eventViewMode.EventStartDate);
            this.eventRepository.Update(existingEvent);
            var result = await this.eventRepository.SaveChangesAsync(cancellationToken);
            return this.Ok(await this.AggregateResponseData(cancellationToken));
        }

        private async Task<EventListResponse> AggregateResponseData(
            CancellationToken cancellationToken = default)
        {
            var events = await this.eventRepository.All().Select(e => new EventViewMode()
            {
                EventStartDate = e.EventStartDate,
                Id = e.Id,
                Name = e.Name,
                OddsForDraw = e.OddsForDraw,
                OddsForFirstTeam = e.OddsForFirstTeam,
                OddsForSecondTeam = e.OddsForSecondTeam,
            }).ToListAsync();

            return new EventListResponse() { Events = events, Succeeded = true };
        }
    }
}
