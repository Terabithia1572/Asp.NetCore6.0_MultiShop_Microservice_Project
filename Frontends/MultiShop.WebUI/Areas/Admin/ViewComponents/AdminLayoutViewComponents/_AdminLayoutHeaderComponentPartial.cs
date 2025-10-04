using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial: ViewComponent // ViewComponent sınıfından türetilir
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public _AdminLayoutHeaderComponentPartial(IMessageService messageService, IUserService userService, ICommentService commentService)
        {
            _messageService = messageService;
            _userService = userService;
            _commentService = commentService;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            // ViewComponent içinde kullanılacak verileri hazırlayabilirsiniz.
            // Örneğin, başlık, meta etiketleri vb. gibi.
            // ViewComponent'ı render etmek için bir view döndürüyoruz.
            var user = await _userService.GetUserInfo(); // Giriş yapmış kullanıcının bilgilerini al
            int messageCount = await _messageService.GetTotalMessageCountByReceiverID(user.ID);
            ViewBag.messageCount = messageCount;

            int totalCommentCount = await _commentService.GetTotalCommentCount();
            ViewBag.totalCommentCount = totalCommentCount;

            return View();
        }
       
    }
}
