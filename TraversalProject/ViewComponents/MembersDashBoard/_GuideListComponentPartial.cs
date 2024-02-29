using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.Dtos.GuideDtos;

namespace TraversalProject.ViewComponents.MembersDashBoard
{
    public class _GuideListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _GuideListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var ressponseMessage = await client.GetAsync("http://localhost:5075/api/Guide/GetGuideList");
            if (ressponseMessage.IsSuccessStatusCode)
            {
                var data = await ressponseMessage.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject<List<ResultGuideDto>>(data);
                return View(jsonResult);
            }

            return View();
        }
    }
}
