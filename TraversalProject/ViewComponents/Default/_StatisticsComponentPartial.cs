using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TraversalProject.Dtos.StatisticsDtos;

namespace TraversalProject.ViewComponents.Default
{
    public class _StatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _StatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var ResponseMessage = await client.GetAsync("http://localhost:5075/api/Statistics");
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var content = await ResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<ResultStatisticsDto>(content);
                if (data != null)
                {
                    ViewBag.HappyCustomerCount = data.HappyCustomerCount.ToString();
                    ViewBag.RotuesCount = data.RoutesCount.ToString();
                    ViewBag.RewardsCount = data.RewardsCount.ToString();
                    ViewBag.TravelGuideCount = data.TravelGuideCount.ToString();

                }
         
                return View();
            }
            return View();
        }
    }
}
