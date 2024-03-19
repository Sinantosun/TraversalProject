using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using TraversalProject.Dtos.AccountDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Account")]
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public AccountController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Route("Index")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var client = _clientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5075/api/Account");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<SelectListItem>>(data);

                ViewBag.v = result;

            }
            return View();

        }
        [Route("Index")]
        [HttpPost]
        public async Task<IActionResult> Index(UpdateAccountDto updateAccountDto)
        {

            var client = _clientFactory.CreateClient();
            var jsondata = JsonConvert.SerializeObject(updateAccountDto);
            StringContent str = new StringContent(jsondata, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:5075/api/Account", str);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["result"] = "Transfer Başarıyla Gerçekleşti.";
                TempData["icon"] = "success";
            }
            else
            {
                var read = await responseMessage.Content.ReadAsStringAsync();
                TempData["result"] = read;
                TempData["icon"] = "danger";
            }


            return RedirectToAction("Index");
        }
    }
}
