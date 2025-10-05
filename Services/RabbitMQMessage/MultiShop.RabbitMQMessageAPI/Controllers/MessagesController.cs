using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MultiShop.RabbitMQMessageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpGet]
        public IActionResult CreateMessage()
        {
            return Ok("Mesajınız Kuyruğa Alınmıştır..");
        }
    }
}
