using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        [HttpGet("HelloWorld")]
        public async Task<IActionResult> HelloWorld()
        {
            await Task.Delay(1000);
            return Ok("Hello, World!");
        }
    }
}
