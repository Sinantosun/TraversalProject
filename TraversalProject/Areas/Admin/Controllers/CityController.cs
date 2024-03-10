using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TraversalProject.Dtos.CityDtos;
using TraversalProject.Dtos.CommentDtos;
using TraversalProject.Dtos.DestinationDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CityController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CityController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }



        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CityList()
        {
            var client = _httpClientFactory.CreateClient();
            var RessponseMessage = await client.GetAsync("http://localhost:5075/api/Destinations");
            if (RessponseMessage.IsSuccessStatusCode)
            {
                var jsonData = await RessponseMessage.Content.ReadAsStringAsync();
                return Json(jsonData);
            }
            return Json("null");

        }
        [HttpPost]
        public async Task<IActionResult> AddCityDestination(CreateDestinationDto createDestinationDto)
        {
            createDestinationDto.Status = true;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(createDestinationDto);
            StringContent strContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5075/api/Destinations", strContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json("success");
            }
            else
            {
                var content = await responseMessage.Content.ReadAsStringAsync();

                return Json(content);
            }
        }


        public async Task<IActionResult> CityListByCityName(string name)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5075/api/Destinations/getDestinationByCityName/{name}");
            var content = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(content);
            }
            else
            {
                return Json("err");
            }
        }

        public async Task<IActionResult> deleteByCityName(string name)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5075/api/Destinations/deleteDestinationByCityName/{name}");
            var content = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(content);
            }
            else
            {
                return Json("err");
            }
        }


        public async Task<IActionResult> updateByCityName(UpdateDestinationDto updateDestinationDto,string name)
        {
            updateDestinationDto.Status = true;
            var client = _httpClientFactory.CreateClient();
            var JsonData = JsonConvert.SerializeObject(updateDestinationDto);
            StringContent strContent = new StringContent(JsonData, Encoding.UTF8, "application/json");
            //http://localhost:5075/api/Destinations/updateDestinationByCityName?name=f
            var responseMessage = await client.PutAsync($"http://localhost:5075/api/Destinations/updateDestinationByCityName?name={name}", strContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return Json("success");
            }
            else
            {
     
                return Json("err");
            }

        }



    }
}
