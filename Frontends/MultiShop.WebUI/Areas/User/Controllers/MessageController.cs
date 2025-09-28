using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")] // Alanı belirt
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService; // Mesaj servisi için alan
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService) // Mesaj servisini yapıcı metoda enjekte et
        {
            _messageService = messageService; // Alanı yapıcı metotta başlat
            _userService = userService;
        }

        public async Task< IActionResult> Inbox()
        {
            var user = await _userService.GetUserInfo(); // Giriş yapmış kullanıcının bilgilerini al
            var values= await _messageService.GetInboxMessageAsync(user.ID); // Kullanıcının gelen kutusu mesajlarını al
            return View(values); // Gelen kutusu mesajlarını görüntüle
        }
        public async Task<IActionResult> Sendbox()
        {
            var user = await _userService.GetUserInfo(); // Giriş yapmış kullanıcının bilgilerini al
            var values = await _messageService.GetSendboxMessageAsync(user.ID); // Kullanıcının gelen kutusu mesajlarını al
            return View(values); // Gelen kutusu mesajlarını görüntüle
        }
    }
}
