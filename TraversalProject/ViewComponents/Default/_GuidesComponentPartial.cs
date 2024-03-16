using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using TraversalProject.Dtos.GuideDtos;

namespace TraversalProject.ViewComponents.Default
{
    public class _GuidesComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _GuidesComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var RessponseMessage = await client.GetAsync("http://localhost:5075/api/Guide/GetRandomGuide");
            if (RessponseMessage.IsSuccessStatusCode)
            {
                var content = await RessponseMessage.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<ResultGuideDto>(content);
                return View(jsonData);
            }
            return View();
        }


    }
}
