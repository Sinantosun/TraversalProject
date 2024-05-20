using BussinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet("Invoke")]
        public IActionResult Invoke(int id=1, string status="")
        {
            return Ok();
        }
    }
}
