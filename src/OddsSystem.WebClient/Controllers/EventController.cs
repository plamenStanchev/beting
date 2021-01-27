namespace OddsSystem.WebClient.Controllers
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using OddsSystem.WebClient.Models;

    public class EventController : Controller
    {
        private const string CreateUri = "https://localhost:44341/api/Event/Create";
        private const string UpdateUrl = "https://localhost:44341/api/Event/Update";
        private const string DeleteUrl = "https://localhost:44341/api/Event/Delete";
        private const string HomeUrl = "/";

        private readonly IHttpClientFactory clientFactory;

        public EventController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]EventViewMode eventViewMode)
        {

            var client = this.clientFactory.CreateClient("Update");
            var stringContent = JsonConvert.SerializeObject(eventViewMode);
            var request = new HttpRequestMessage(HttpMethod.Patch, UpdateUrl)
                {
                    Content = new StringContent(stringContent, Encoding.UTF8, "application/json"),
                };
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            return this.Ok(true);
        }

        [HttpPost]

        public async Task<IActionResult> Create([FromBody]EventViewMode eventViewMode)
        {
            var client = this.clientFactory.CreateClient("Create");
            var stringContent = JsonConvert.SerializeObject(eventViewMode);

            var request = new HttpRequestMessage(HttpMethod.Post, CreateUri)
            {
                Content = new StringContent(stringContent, Encoding.UTF8, "application/json"),
            };

            var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            return this.Ok(true);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var client = this.clientFactory.CreateClient("Delete");
            var stringContent = JsonConvert.SerializeObject(id);

            var request = new HttpRequestMessage(HttpMethod.Delete, DeleteUrl)
                {
                    Content = new StringContent(stringContent, Encoding.UTF8, "application/json"),
                };
            var result = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            return this.RedirectPermanent(HomeUrl);
        }
    }
}
