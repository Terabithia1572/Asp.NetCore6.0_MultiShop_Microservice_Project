using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Message.DTOs;
using MultiShop.Message.Services;

namespace MultiShop.Message.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private readonly IUserMessageService _userMessageService;

        public UserMessagesController(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessage()
        {
            var values = await _userMessageService.GetAllMessageAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync(CreateMessageDTO createMessageDTO)
        {
            await _userMessageService.CreateMessageAsync(createMessageDTO);
            return Ok("Mesaj Başarıyla Eklendi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIDMessageAsync(int id)
        {
            var values = await _userMessageService.GetByIDMessageAsync(id);
            return Ok(values);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageAsync(int id)
        {
            await _userMessageService.DeleteMessageAsync(id);
            return Ok("Mesaj Başarıyla Silindi");
        }
        [HttpGet("inbox/{id}")]
        public async Task<IActionResult> GetInboxMessageAsync(string id)
        {
            var values = await _userMessageService.GetInboxMessageAsync(id);
            return Ok(values);
        }
        [HttpGet("sendbox/{id}")]
        public async Task<IActionResult> GetSendboxMessageAsync(string id)
        {
            var values = await _userMessageService.GetSendboxMessageAsync(id);
            return Ok(values);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMessageAsync(UpdateMessageDTO updateMessageDTO)
        {
            await _userMessageService.UpdateMessageAsync(updateMessageDTO);
            return Ok("Mesaj Başarıyla Güncellendi");
        }
    }
}
