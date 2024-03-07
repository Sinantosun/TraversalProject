
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using TraversalProject.Dtos.CommentDtos;
using TraversalProject.Dtos.DestinationDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Destinations")]
    public class DestinationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DestinationsController(IHttpClientFactory client)
        {
            _httpClientFactory = client;
        }
        [Route("Index")]
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
        [Route("AddDestination")]
        public IActionResult AddDestination()
        {
            return View();
        }

        [HttpPost]
        [Route("AddDestination")]
        public async Task<IActionResult> AddDestination(CreateDestinationDto createDestinationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(createDestinationDto);
            StringContent strContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5075/api/Destinations", strContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                
                return RedirectToAction("Index", "Destinations", new { area = "Admin" });
            }
            else
            {

                var content = await responseMessage.Content.ReadAsStringAsync();
                var contentResult = JsonConvert.DeserializeObject<List<ResultNotificationDto>>(content);
                foreach (var item in contentResult)
                {
                    ModelState.AddModelError(item.PropertyName, item.Description);
                }
            }
            return View();
        }
        [Route("DeleteDestination/{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5075/api/Destinations?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Destinations", new { area = "Admin" });
            }
            return View();
        }
        [HttpGet]
        [Route("UpdateDestination/{id}")]
        public async Task<IActionResult> UpdateDestination(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var RessponseMessage = await client.GetAsync($"http://localhost:5075/api/Destinations/{id}");
            if (RessponseMessage.IsSuccessStatusCode)
            {
                var content = await RessponseMessage.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<UpdateDestinationDto>(content);
                return View(jsonData);
            }
            return View();
        }
        [HttpPost]
        [Route("UpdateDestination/{id}")]
        public async Task<IActionResult> UpdateDestination(UpdateDestinationDto updateDestinationDto)
        {
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateDestinationDto);
            StringContent strContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5075/api/Destinations", strContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.StatusForUpdatedingDestination = "Başarıyla Güncellendi";
                return RedirectToAction("Index", "Destinations", new { area = "Admin" });
            }
            else
            {
                ViewBag.StatusForUpdatedingDestination = "Güncellenemedi.!";
            }
            return RedirectToAction("Index");
        }
    }
}
