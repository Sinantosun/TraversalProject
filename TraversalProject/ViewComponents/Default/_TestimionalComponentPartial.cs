using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalProject.TestimonailDtos;

namespace TraversalProject.ViewComponents.Default
{
    public class _TestimionalComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _TestimionalComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var ResponseMessage = await client.GetAsync("http://localhost:5075/api/Testimonial");
            if (ResponseMessage.IsSuccessStatusCode)
            {
                var data = await ResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(data);
                return View(result);
            }
            return View();
        }
    }
}
