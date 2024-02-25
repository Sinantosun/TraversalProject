using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using TraversalProject.Dtos.CommentDtos;

namespace TraversalProject.Controllers
{
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
            await client.PostAsync("http://localhost:5075/api/Comment", str);

            return RedirectToAction("Index", "Destination");
        }
    }
}
