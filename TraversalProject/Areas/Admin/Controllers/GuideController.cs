using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.Dtos.GuideDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuideController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GuideController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var message = await client.GetAsync("http://localhost:5075/api/Guide/GetGuideList");
            if (message.IsSuccessStatusCode)
            {
                var data = await message.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultGuideDto>>(data);
                return View(result);

            }
            return View();
        }
    }
}
