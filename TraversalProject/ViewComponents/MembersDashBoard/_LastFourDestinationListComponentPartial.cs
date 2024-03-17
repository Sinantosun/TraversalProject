using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.Dtos.DestinationDtos;

namespace TraversalProject.ViewComponents.MembersDashBoard
{
    public class _LastFourDestinationListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _LastFourDestinationListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //http://localhost:5075/api/Destinations/getFourLastDestination
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var message = await client.GetAsync("http://localhost:5075/api/Destinations/getFourLastDestination");
            if (message.IsSuccessStatusCode)
            {
                var data = await message.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultDestinationDto>>(data);
                return View(result);
            }

            return View();
        }
    }
}
