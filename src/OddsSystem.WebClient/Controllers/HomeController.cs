namespace OddsSystem.WebClient.Controllers
{
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
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

        public async Task<IActionResult> Index()
        {
            var client = this.clientFactory.CreateClient("GetEvents");
            var request = new HttpRequestMessage(HttpMethod.Get,
                ListUri);
            var response = await client.SendAsync(request);

            return this.View();
        }

        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
