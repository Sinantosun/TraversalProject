using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.Dtos.ReservationDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Route("Admin/Reservations")]
    [Area("Admin")]
    public class ReservationsController : Controller
    {
        //http://localhost:5075/api/Resarvation/GetReservationList

        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5075/api/Resarvation/GetReservationList");
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultReservationDto>>(json);
                return View(result);
            }
            return View();
        }
    }
}
