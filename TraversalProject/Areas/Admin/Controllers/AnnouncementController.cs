using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TraversalProject.Dtos.AnnouncementDtos;
using TraversalProject.Dtos.CommentDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    [Route("Admin/Announcement")]
    public class AnnouncementController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AnnouncementController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5075/api/Announcement");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultAnnouncumentDto>>(content);
                return View(result);
            }
            return View();
        }
        [Route("AddAnnouncement")]
        [HttpGet]
        public IActionResult AddAnnouncement()
        {
            return View();
        }
        [Route("AddAnnouncement")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddAnnouncement(CreateAnnouncementDto createAnnouncementDto)
        {
            var client = _httpClientFactory.CreateClient();
            createAnnouncementDto.Date = Convert.ToDateTime(DateTime.Now.ToString("d"));
            var data = JsonConvert.SerializeObject(createAnnouncementDto);
            StringContent str = new StringContent(data, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5075/api/Announcement", str);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Result"] = "Yeni Duyuru Başarıyıyla Eklendi.";
                TempData["Icon"] = "info";

            }
            else
            {
                var read = await responseMessage.Content.ReadAsStringAsync();
                var convertData = JsonConvert.DeserializeObject<List<ResultNotificationDto>>(read);
                foreach (var item in convertData)
                {
                    ModelState.AddModelError(item.PropertyName, item.Description);
                }
            }
            return RedirectToAction("AddAnnouncement", "Announcement", new { area = "Admin" });
        }

        [Route("DeleteAnnouncement/{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:5075/api/Announcement?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Result"] = "Başarıyla Silindi.";
                TempData["Icon"] = "info";
            }
            else
            {
                TempData["Result"] = "Duyuru silinemedi bir hata oluştu.";
                TempData["Icon"] = "danger";
            }
            return RedirectToAction("Index", "Announcement", new { area = "Admin" });
        }


        [Route("UpdateAnnouncement/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateAnnouncement(int id)
        {

            //http://localhost:5075/api/Announcement/5
            //http://localhost:5075/api/Announcement


            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5075/api/Announcement/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UpdateAnnouncumentDto>(data);
                return View(result);
            }

            return View();
        }
        [Route("UpdateAnnouncement/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAnnouncement(UpdateAnnouncumentDto updateAnnouncumentDto)
        {

            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(updateAnnouncumentDto);
            StringContent str = new StringContent(data,Encoding.UTF8,"application/json");
            var responseMessage = await client.PutAsync($"http://localhost:5075/api/Announcement",str);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Result"] = "Başarıyla Güncellendi.";
                TempData["Icon"] = "info";
                return RedirectToAction("Index", "Announcement", new { area = "Admin" });
            }
            else
            {
                TempData["Result"] = "Güncellemede bir hata oluştu güncellenemedi..";
                TempData["Icon"] = "danger";
                return RedirectToAction("Index", "Announcement", new { area = "Admin" });
            }
        }
    }
}
