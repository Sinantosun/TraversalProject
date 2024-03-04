using DataAccsesLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TraversalProject.Dtos.GuideDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Guide")]
    public class GuideController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GuideController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
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
        [Route("AddGuide")]
        [HttpGet]
        public IActionResult AddGuide()
        {

            return View();
        }
        [Route("AddGuide")]
        [HttpPost]
        public async Task<IActionResult> AddGuide(CreateGuideDto createGuideDto)
        {
            createGuideDto.GuideListImage = "test";
            createGuideDto.Status = true;
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(createGuideDto);
            StringContent str = new StringContent(data, Encoding.UTF8, "application/json");
            var ResponesMesssage = await client.PostAsync("http://localhost:5075/api/Guide", str);
            if (ResponesMesssage.IsSuccessStatusCode)
            {
                ViewBag.Result = "Başarıyla Eklendi.";
                ViewBag.Icon = "success";
            }
            else
            {
                ViewBag.Icon = "dark";
                ViewBag.Result = "Kayıt Sırasında Bir Hata Oluştu.";
            }
            return View();
        }
        [Route("EditGuide/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditGuide(int id)
        {
            //http://localhost:5075/api/Guide/4
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:5075/api/Guide/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UpdateGuideDto>(jsondata);
                return View(result);

            }

            return View();
        }
        [HttpPost]
        [Route("EditGuide/{id}")]
        public async Task<IActionResult> EditGuide(UpdateGuideDto updateGuideDto)
        {

            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(updateGuideDto);
            StringContent str = new StringContent(data, Encoding.UTF8, "application/json");
            var ResponesMesssage = await client.PutAsync("http://localhost:5075/api/Guide", str);
            if (ResponesMesssage.IsSuccessStatusCode)
            {
                ViewBag.Result = "Başarıyla Eklendi.";
                ViewBag.Icon = "success";
                return RedirectToAction("Index", "Guide", new { area = "Admin" });
            }
            else
            {
                ViewBag.Icon = "dark";
                ViewBag.Result = "Kayıt Sırasında Bir Hata Oluştu.";
                return RedirectToAction("EditGuide", "Guide", new { area = "Admin" });
            }

        }

        [Route("changeStatusToTrue/{id}")]
        public IActionResult changeStatusToTrue(int id)
        {
            using var context = new Context();
            var value = context.Guides.Find(id);
            value.Status = true;
            context.SaveChanges();
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }

        [Route("changeStatusToFalse/{id}")]
        public IActionResult changeStatusToFalse(int id)
        {
            using var context = new Context();
            var value = context.Guides.Find(id);
            value.Status = false;
            context.SaveChanges();
            return RedirectToAction("Index", "Guide", new { area = "Admin" });
        }
    }
}
