namespace OddsSystem.Tests.PublicApiTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using MockQueryable.Moq;
    using Moq;
    using OddsSystem.Core.Entities.Models;
    using OddsSystem.Core.Interfaces.Repository;
    using OddsSystem.PublicApi.Features.Events;
    using OddsSystem.PublicApi.Features.Events.Models;
    using Xunit;

    public class EventControllerTests
    {
        public EventController controller;
        public Mock<IDeletableEntityRepository<Event>> repo;
        public List<Event> data;

        public EventControllerTests()
        {
            this.repo = new Mock<IDeletableEntityRepository<Event>>();
            this.controller = new EventController(this.repo.Object);
        }

        //EventStartDate = e.EventStartDate,
        //        Id = e.Id,
        //        Name = e.Name,
        //        OddsForDraw = e.OddsForDraw,
        //        OddsForFirstTeam = e.OddsForFirstTeam,
        //        OddsForSecondTeam = e.OddsForSecondTeam,

        [Fact]
        public void ShudListItemsWithOkResponse()
        {
            var dbEvents = new List<Event>().AsQueryable().BuildMockDbSet();
            this.repo.Setup(a => a.All()).Returns(dbEvents.Object);
            var okResult = this.controller.List();

            Assert.IsType<OkObjectResult>(okResult.GetAwaiter().GetResult().Result);
        }

        [Fact]
        public void UpdateThrowsExeption()
        {
            var eventModel = new EventViewMode()
            {
                Id = 2,
                EventStartDate = DateTime.Parse("2021 - 01 - 27T12:33"),
                OddsForDraw = 1.00m,
                OddsForFirstTeam = 1.00m,
                OddsForSecondTeam = 2.00m,
                Name = "Some",
            };

            this.SetUpMockRepoDbSet(new List<Event>());

            Assert.ThrowsAsync<NullReferenceException>(() => this.controller.Update(eventModel));
        }

        [Fact]
        public void DelteReturnsOK()
        {
            this.SetUpMockRepoDbSet(new List<Event>());

            var okResult = this.controller.Delete(1);
            Assert.IsType<OkResult>(okResult.GetAwaiter().GetResult());
        }

        [Fact]
        public void ShudCreateEvent()
        {
            var eventModel = new EventViewMode()
            {
                EventStartDate = DateTime.Parse("2021 - 01 - 27T12:33"),
                OddsForDraw = 1.00m,
                OddsForFirstTeam = 1.00m,
                OddsForSecondTeam = 2.00m,
                Name = "Some",
            };

            this.SetUpMockRepoDbSet(new List<Event>());

            var okResult = this.controller.Create(eventModel);

            Assert.IsType<OkObjectResult>(okResult.GetAwaiter().GetResult().Result);
        }

        private void SetUpMockRepoDbSet(IEnumerable<Event> events)
        {
            var dbEvents = events.AsQueryable().BuildMockDbSet();
            this.repo.Setup(a => a.All()).Returns(dbEvents.Object);
        }

        
    }
}
