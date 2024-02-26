using BussinessLayer.AbstractValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using TraversalProject.Dtos.ReservationDtos;

namespace TraversalProject.Areas.Members.Controllers
{
    [Area("Members")]
    public class ReservationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDestinationService _destinationService;

        public ReservationController(IHttpClientFactory httpClientFactory, IDestinationService destinationService)
        {
            _httpClientFactory = httpClientFactory;
            _destinationService = destinationService;
        }

        public IActionResult MyCurrentReservation()
        {
            return View();
        }
        public IActionResult MyOldReservation()
        {
            return View();
        }

        [Area("Members")]
        [HttpGet]
        public IActionResult NewReservation()
        {
            List<SelectListItem> value = (from x in _destinationService.TGetList() select new SelectListItem { Text = x.City, Value = x.DestinationID.ToString() }).ToList();
            ViewBag.v = value;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> NewReservation(CreateResarvationDto createResarvationDto)
        {
            //
            createResarvationDto.Status = "Onay Bekliyor";
            createResarvationDto.AppUserId = 7;

            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(createResarvationDto);
            StringContent str = new StringContent(data, Encoding.UTF8, "application/json");
            var x =  await client.PostAsync("http://localhost:5075/api/Resarvation", str);
            return View("MyCurrentReservation");
            

            
        }
    }
}
