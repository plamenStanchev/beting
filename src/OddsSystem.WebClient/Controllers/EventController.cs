namespace OddsSystem.WebClient.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class EventController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        public EventController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [HttpPatch]
        public async Task<IActionResult> Update()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create()
        {

        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {

        }
    }
}
