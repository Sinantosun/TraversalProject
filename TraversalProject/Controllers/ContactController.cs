using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult _AddContact()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> _AddContact(CreateContactUSDto createContactUSDto)
        {
            createContactUSDto.MessageStatus = true;
            createContactUSDto.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(createContactUSDto);
            StringContent str = new StringContent(data, Encoding.UTF8, "application/json");
            
            var message = await client.PostAsync("http://localhost:5075/api/ContactUs",str);
            if (message.IsSuccessStatusCode)
            {
                TempData["result"] = "Mesajınız İletildi Teşekkür Ederiz.";
                TempData["Icon"] = "success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["result"] = "Üzgünüm :( bir hata oluştu.";
                TempData["Icon"] = "danger";
                return RedirectToAction("Index");
            }
         
        }

  


    }
}
