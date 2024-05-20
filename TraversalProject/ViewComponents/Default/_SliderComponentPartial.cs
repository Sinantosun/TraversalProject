using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TraversalProject.Dtos.DestinationDtos;

namespace TraversalProject.ViewComponents.Default
{
    public class _SliderComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _SliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5075/api/Destinations");
            if (responseMessage.IsSuccessStatusCode)
            {
                var list = await responseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ResultDestinationDto>>(list);
                List<SelectListItem> liste = (from x in data
                                              select new SelectListItem
                                              {
                                                  Text = x.City,
                                                  Value = x.DestinationID.ToString()
                                              }).ToList();
                TempData["sliderResult"] = liste;

            }
            return View();
        }
    }
}
