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
        public async Task<ActionResult<EventListResponse>> List(CancellationToken cancellationToken = default)
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

            var eventResponse = new EventListResponse()
            {
                Events = events,
                Succeeded = true,
            };

            return this.Ok(eventResponse);
        }

        [HttpPost]
        [Route(nameof(Create))]

        public async Task<ActionResult<EventModelResponse>> Create(
            EventViewMode eventViewMode,
            CancellationToken cancellationToken = default)
        {
            var @event = new Event(
                eventViewMode.Name,
                eventViewMode.OddsForFirstTeam,
                eventViewMode.OddsForFirstTeam,
                eventViewMode.OddsForSecondTeam,
                eventViewMode.EventStartDate);

            await this.eventRepository.AddAsync(@event);
            var eventResponse = new EventListResponse();
            var result = await this.eventRepository.SaveChangesAsync();
            var eventModelResponse = this.MapResponseModel(@event);

            if (result == 0)
            {
                eventModelResponse.ErrorMesage = "Ther was an Error";
                eventModelResponse.Succeeded = false;
            }

            return this.Ok(eventModelResponse);
        }

        [HttpDelete]
        [Route(nameof(Delete))]
        public async Task<ActionResult> Delete(
            [FromBody] int Id,
            CancellationToken cancellationToken = default)
        {
            var existingEvent = await this.eventRepository.All()
                .FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
            if (existingEvent == null)
            {
                return this.Ok();
            }

            this.eventRepository.Delete(existingEvent);
            await this.eventRepository.SaveChangesAsync(cancellationToken);
            return this.Ok();
        }

        [HttpPatch]
        [Route(nameof(Update))]
        public async Task<ActionResult<EventModelResponse>> Update(
            [FromBody]EventViewMode eventViewMode,
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
            var eventModelResponse = this.MapResponseModel(existingEvent);

            return this.Ok(eventModelResponse);
        }

        private EventModelResponse MapResponseModel(Event @event)
        {
            var eventModelResponse = new EventModelResponse()
            {
                Id = @event.Id,
                EventStartDate = @event.EventStartDate,
                Name = @event.Name,
                OddsForDraw = @event.OddsForDraw,
                OddsForFirstTeam = @event.OddsForFirstTeam,
                OddsForSecondTeam = @event.OddsForSecondTeam,
                Succeeded = true,
            };
            return eventModelResponse;
        }
    }
}
