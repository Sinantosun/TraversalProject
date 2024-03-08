using BussinessLayer.AbstractValidator;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using TraversalProject.Dtos.CommentDtos;
using TraversalProject.Dtos.ReservationDtos;

namespace TraversalProject.Areas.Members.Controllers
{
    [Area("Members")]
    [Route("Members/Reservation")]
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
        [Route("MyCurrentReservation")]
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
        [Route("MyOldReservation")]
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
        [Route("MyApprovalReservation")]
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

        void loadDropdown()
        {
            List<SelectListItem> value = (from x in _destinationService.TGetList() select new SelectListItem { Text = x.City, Value = x.DestinationID.ToString() }).ToList();

            ViewBag.v = value;
        }


        [Route("NewReservation")]
        [HttpGet]
        public IActionResult NewReservation()
        {
            loadDropdown();
            return View();
        }
        [Route("NewReservation")]
        [HttpPost]
        public async Task<IActionResult> NewReservation(CreateResarvationDto createResarvationDto)
        {
            createResarvationDto.Status = "Onay Bekliyor";
            createResarvationDto.AppUserId = 15;

            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(createResarvationDto);
            StringContent str = new StringContent(data, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5075/api/Resarvation", str);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MyApprovalReservation", "Reservation", new { area = "Members" });
            }
            else
            {
                var erorrListData = await responseMessage.Content.ReadAsStringAsync();
                var erorrListResult = JsonConvert.DeserializeObject<List<ResultNotificationDto>>(erorrListData);
                foreach (var item in erorrListResult)
                {
                    ModelState.AddModelError(item.PropertyName, item.Description);
                }

            }
            loadDropdown();
            return View();




        }
    }
}
