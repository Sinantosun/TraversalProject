using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.Dtos.IdentityDtos;
using TraversalProject.Dtos.ReservationDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/User")]
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5075/api/AppUser");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultUserDto>>(data);
                return View(result);
            }
            return View();
        }

        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5075/api/AppUser?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            return View();
        }

        [HttpGet]
        [Route("CommentUser/{id}")]
        public async Task<IActionResult> CommentUser(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5075/api/AppUser/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultUserDto>>(data);
                return View(result);
            }
            return View();
        }

        [HttpGet]
        [Route("ReseravtionUser/{id}")]
        public async Task<IActionResult> ReseravtionUser(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5075/api/AppUser/UserReservationLists/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultReservationByIdDto>>(data);
                return View(result);
            }
            return View();
        }

    }
}
