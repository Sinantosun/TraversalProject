using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.CommentDtos;

namespace TraversalProject.ViewComponents.Comment
{
    public class _CommentDestinationComponentPartial : ViewComponent
    {


        private readonly IHttpClientFactory _httpClientFactory;

        public _CommentDestinationComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var RessponseMessage = await client.GetAsync($"http://localhost:5075/api/Comment/{id}");
            if (RessponseMessage.IsSuccessStatusCode)
            {
                var content = await RessponseMessage.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<List<FilterCommentDto>>(content);
                return View(jsonData);
            }
            return View();
        }
    }
}
