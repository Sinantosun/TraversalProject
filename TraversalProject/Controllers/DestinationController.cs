using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.DestinationDtos;

namespace TraversalProject.Controllers
{
    public class DestinationController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public DestinationController(IHttpClientFactory client)
        {
            _httpClientFactory = client;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var RessponseMessage = await client.GetAsync("http://localhost:5075/api/Destinations");
            if (RessponseMessage.IsSuccessStatusCode)
            {
                var content = await RessponseMessage.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<List<ResultDestinationDto>>(content);
                return View(jsonData);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DestinationDetails(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var RessponseMessage = await client.GetAsync($"http://localhost:5075/api/Destinations/{id}");
            if (RessponseMessage.IsSuccessStatusCode)
            {
                var content = await RessponseMessage.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<ResultByIdForDestinationDto>(content);
                ViewBag.FilterId = id;
                return View(jsonData);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public IActionResult DestinationDetails(string y)
        {
            return View();
        }
    }
}
