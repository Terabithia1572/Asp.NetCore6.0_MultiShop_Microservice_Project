using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;

namespace MultiShop.Message.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessageStatisticsController : ControllerBase
    {
        private readonly MessageContext _messageContext;

        public UserMessageStatisticsController(MessageContext messageContext)
        {
            _messageContext = messageContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessageTotalCount()
        {
            int values = await _messageContext.UserMessages.CountAsync();
            return Ok(values);
        }
    }
}
