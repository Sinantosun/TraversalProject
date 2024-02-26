using BussinessLayer.AbstractValidator;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;


        public ReservationController(IHttpClientFactory httpClientFactory, IDestinationService destinationService, UserManager<AppUser> userManager)
        {
            _httpClientFactory = httpClientFactory;
            _destinationService = destinationService;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyCurrentReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var client = _httpClientFactory.CreateClient();
            var ResponseMessage = await client.GetAsync($"http://localhost:5075/api/Resarvation/GetCurrentReservations/id?id={values.Id}");
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var data = await ResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultReservationByIdDto>>(data);
                return View(result);
            }
            return View();
        }
        public async Task<IActionResult> MyOldReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var client = _httpClientFactory.CreateClient();
            var ResponseMessage = await client.GetAsync($"http://localhost:5075/api/Resarvation/GetOldReservation/id?id={values.Id}");
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var data = await ResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultReservationByIdDto>>(data);
                return View(result);
            }
            return View();
        }
        public async Task<IActionResult> MyApprovalReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var client = _httpClientFactory.CreateClient();
            var ResponseMessage = await client.GetAsync($"http://localhost:5075/api/Resarvation/GetApproveReservations/id?id={values.Id}");
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var data = await ResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultReservationByIdDto>>(data);
                return View(result);
            }
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
            var x = await client.PostAsync("http://localhost:5075/api/Resarvation", str);
            return View("MyCurrentReservation");



        }
    }
}
