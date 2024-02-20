using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.FeaturesDtos;

namespace TraversalProject.ViewComponents.Default
{
    public class _FeaturesComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FeaturesComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5075/api/Features");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatrueDto>>(content);
                return View(values);
            }
            return View();
        }
    }
}
