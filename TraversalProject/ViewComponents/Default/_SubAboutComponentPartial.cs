using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.SubAboutDtos;

namespace TraversalProject.ViewComponents.Default
{
    public class _SubAboutComponentPartial : ViewComponent
    {
        private IHttpClientFactory _httpClientFactory;

        public _SubAboutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5075/api/SubAbout");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultSubAboutDto>>(data);
                return View(result);
            }

            return View();
        }
    }
}
