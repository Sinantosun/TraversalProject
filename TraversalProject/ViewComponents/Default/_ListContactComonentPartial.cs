using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.Dtos.ContactDtos;

namespace TraversalProject.ViewComponents.Default
{
    public class _ListContactComonentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ListContactComonentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //http://localhost:5075/api/Contact
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5075/api/Contact");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultContactDto>>(data);
                foreach (var item in result)
                {
                    ViewBag.MapLocation = item.MapLocation;
                }
                return View(result);
            }
            return View();
        }
    }
}
