using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.Dtos.DestinationDtos;

namespace TraversalProject.ViewComponents.Default
{
    public class _PopularDestinationsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _PopularDestinationsComponentPartial(IHttpClientFactory client)
        {
            _httpClientFactory = client;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var RessponseMessage = await client.GetAsync("http://localhost:5075/api/Destinations/DestinationForDefaultPage");
            if (RessponseMessage.IsSuccessStatusCode)
            {
                var content = await RessponseMessage.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<List<ResultDestinationDto>>(content);
                return View(jsonData);
            }
            return View();
        }
    }
}
