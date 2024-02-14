using BussinessLayer.Abstract;
using DtoLayer.StatisticsDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IDestinationService _destinationService;

        public StatisticsController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        [HttpGet]
        public IActionResult GetStatisticsCount()
        {
            ResultStatisticsDto result = new ResultStatisticsDto()
            {
                HappyCustomerCount = 250,
                RewardsCount = 25,
                TravelGuideCount = 0,
                RoutesCount=_destinationService.TGetDestinationService()
            };

            return Ok(result);
        }
    }
}
