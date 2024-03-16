using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace TraversalProject.ViewComponents.Default
{
    public class _AddContactComponentPartial:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _AddContactComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var message = await client.GetAsync("http://localhost:5075/api/ContactUs");
            //http://localhost:5075/api/ContactUs
            return View();
        }
    }
}
