using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.Dtos.DestinationDtos;
using static TraversalProject.Dtos.BookHotelDtos.ResultBookingHotelDto;

namespace TraversalProject.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchDestination(SearchDestinationDto searchDestinationDto)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5075/api/Destinations/SearchDestination?CityID={searchDestinationDto.DestinationID}&datetime={searchDestinationDto.DestinationDate}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultDestinationDto>>(data);
                return View(result);
            }
            return View();
        }
    }
}
