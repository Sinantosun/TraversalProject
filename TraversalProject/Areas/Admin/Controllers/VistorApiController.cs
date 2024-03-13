using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TraversalProject.Dtos.VisitorDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/VistorApi")]
    [AllowAnonymous]
    public class VistorApiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VistorApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5140/api/Visitor");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsondata = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultVisitorDto>>(jsondata);
                return View(values);
            }
            return View();
        }

        [Route("CreateVisitor")]
        [HttpGet]
        public IActionResult CreateVisitor()
        {
            return View();
        }
        [Route("CreateVisitor")]
        [HttpPost]
        public async Task<IActionResult> CreateVisitor(CreateVisitorDto createVisitorDto)
        {

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createVisitorDto);
            StringContent str = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:5140/api/Visitor ", str);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.Result = "Yeni Ziyaretçi Kaydı Başarıyla Eklendi";
            }
            else
            {
                ViewBag.Result = "Ekleme sırasında bir hata oluştu";
            }

            return View();
        }

        [Route("EditVistor/{id}")]
        [HttpGet]
        public async Task<IActionResult> EditVistor(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responesMessage = await client.GetAsync($"http://localhost:5140/api/Visitor/{id}");
            if (responesMessage.IsSuccessStatusCode)
            {
                var jsonData = await responesMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateVisitorDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [Route("EditVistor")]
        [HttpPost]
        public async Task<IActionResult> EditVistor(UpdateVisitorDto updateVisitorDto)
        {
            //http://localhost:5140/api/Visitor add
            //http://localhost:5140/api/Visitor put
            //http://localhost:5140/api/Visitor/8 delete
            //http://localhost:5140/api/Visitor/8 by id

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateVisitorDto);
            StringContent str = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5140/api/Visitor", str);
            if (responseMessage.IsSuccessStatusCode)
            {
                ViewBag.Result = "Ziyaretçi Kaydı Başarıyla Güncellendi";
            }
            else
            {
                ViewBag.Result = "Ekleme sırasında bir hata oluştu";
            }

            return View();
        }
        [Route("DeleteVisitor/{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {

            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.DeleteAsync($"http://localhost:5140/api/Visitor/{id}");
            return RedirectToAction("Index", "VistorApi", new { @area = "Admin" });
        }
    }
}
