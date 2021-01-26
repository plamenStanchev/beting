namespace OddsSystem.WebClient.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using OddsSystem.WebClient.Models;

    public class HomeController : Controller
    {
        private const string ListUri = "https://localhost:44341/api/Event/List";
        private readonly ILogger<HomeController> logger;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.clientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Preview()
        {
            var events = await this.GetEventData();
            return this.View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var events = await this.GetEventData();
            return this.View(events);
        }

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        private async Task<IEnumerable<EventViewMode>> GetEventData()
        {
            var client = this.clientFactory.CreateClient("GetList");
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                ListUri);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            var resultBytes = await response.Content.ReadAsByteArrayAsync();
            var resultstring = Encoding.UTF8.GetString(resultBytes);

            var events = JsonConvert.DeserializeObject<EventListModelResponse>(resultstring);

            return events.Events;
        }
    }
}
