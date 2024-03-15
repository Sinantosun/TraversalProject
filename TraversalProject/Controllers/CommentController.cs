using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using TraversalProject.Dtos.CommentDtos;

namespace TraversalProject.Controllers
{
    [AllowAnonymous]
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public PartialViewResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommandDto createCommand)
        {
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(createCommand);
            StringContent str = new StringContent(data, Encoding.UTF8, "application/json");
            var responseMesssage = await client.PostAsync("http://localhost:5075/api/Comment", str);
            if (responseMesssage.IsSuccessStatusCode)
            {
                TempData["icon"] = "success";
                TempData["CommentResult"] = "Yorum Eklendi";
                return RedirectToAction("DestinationDetails", "Destination", new { @id = createCommand.DestinationID });
            }
            else
            {
                TempData["icon"] = "danger";
                var readContent = await responseMesssage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultNotificationDto>>(readContent);
                foreach (var item in result)
                {
                    TempData["CommentResult"] += item.Description + "<br>";
                }
            }
            return RedirectToAction("DestinationDetails", "Destination", new { @id = createCommand.DestinationID });

        }
    }
}
