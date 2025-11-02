using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;

namespace MultiShop.WebUI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public _AdminLayoutHeaderComponentPartial(
            IMessageService messageService,
            IUserService userService,
            ICommentService commentService)
        {
            _messageService = messageService;
            _userService = userService;
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserInfo(); // 🔹 Giriş yapan kullanıcı bilgisi

            if (user != null)
            {
                ViewBag.UserName = $"{user.Name} {user.Surname}";
                ViewBag.UserImage = !string.IsNullOrEmpty(user.ProfileImageUrl)
                    ? user.ProfileImageUrl
                    : "/profile-images/default-avatar.png"; // fallback
            }
            else
            {
                ViewBag.UserName = "Misafir";
                ViewBag.UserImage = "/profile-images/default-avatar.png";
            }

            // Mesaj & yorum sayısı
            ViewBag.MessageCount = await _messageService.GetTotalMessageCountByReceiverID(user?.ID ?? "");
            ViewBag.TotalCommentCount = await _commentService.GetTotalCommentCount();

            return View();
        }
    }
}
