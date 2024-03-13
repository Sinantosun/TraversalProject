using BussinessLayer.AbstractValidator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using TraversalProject.Dtos.BookingExchangeDtos;

namespace TraversalProject.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    public class ApiExchangeController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ResultRapidAPIExchangeDto> resultRapidAPIExchangeDto = new List<ResultRapidAPIExchangeDto>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency=TRY&locale=en-gb"),
                Headers =
            {
                { "X-RapidAPI-Key", "26a94699b7mshf92c10fadb7e461p155d6cjsn8e4aae7a4ad2" },
                { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResultRapidAPIExchangeDto>(body);
                return View(result.exchange_rates);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string currency)
        {
            List<ResultRapidAPIExchangeDto> resultRapidAPIExchangeDto = new List<ResultRapidAPIExchangeDto>();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com.p.rapidapi.com/v1/metadata/exchange-rates?currency={currency}&locale=en-gb"),
                Headers =
            {
                { "X-RapidAPI-Key", "26a94699b7mshf92c10fadb7e461p155d6cjsn8e4aae7a4ad2" },
                { "X-RapidAPI-Host", "booking-com.p.rapidapi.com" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ResultRapidAPIExchangeDto>(body);
                return View(result.exchange_rates);
            }
        }



    }
}
