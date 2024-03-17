using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using TraversalProject.Dtos.CommentDtos;
using TraversalProject.Dtos.ContactUsDtos;

namespace TraversalProject.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateContactUSDto createContactUSDto)
        {
            createContactUSDto.MessageStatus = true;
            createContactUSDto.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(createContactUSDto);
            StringContent str = new StringContent(data, Encoding.UTF8, "application/json");

            var message = await client.PostAsync("http://localhost:5075/api/ContactUs", str);
            if (message.IsSuccessStatusCode)
            {
                TempData["result"] = "Mesajınız İletildi Teşekkür Ederiz.";
                TempData["Icon"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                var readContent = await message.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<List<ResultNotificationDto>>(readContent);
                foreach (var item in jsonData)
                {
                    ModelState.AddModelError(item.PropertyName, item.Description);
                }
                TempData["result"] = "mesajınız gönderilemedi.";
                TempData["Icon"] = "danger";
                return View();
            }
        }


    }
}
