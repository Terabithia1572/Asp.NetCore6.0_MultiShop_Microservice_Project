using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Basket.DTOs.BasketTotalDTOs;
using MultiShop.Basket.LoginServices;
using MultiShop.Basket.Services;

namespace MultiShop.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService; // IBasketService, sepet işlemlerini yönetir
        private readonly ILoginService _loginService; // ILoginService, kullanıcının kimliğini yönetir

        public BasketsController(IBasketService basketService, ILoginService loginService) // BasketsController, sepet işlemlerini yöneten bir denetleyicidir
        {
            _basketService = basketService;// IBasketService, sepet işlemlerini yönetir
            _loginService = loginService; // ILoginService, kullanıcının kimliğini yönetir
        }
        [HttpGet]
        public async Task<IActionResult> GetMyBasketDetail() // Sepeti getirir
        {
            var userID = _loginService.GetUserID; // Kullanıcının ID'sini alır
            var basket = await _basketService.GetBasket(userID); // Sepeti alır
            return Ok(basket); // Sepeti döner
        }
        [HttpPost]
        public async Task<IActionResult> SaveMyBasket(BasketTotalDTO basketTotalDTO) // Sepeti kaydeder
        {
            basketTotalDTO.UserID = _loginService.GetUserID; // Kullanıcının ID'sini sepet nesnesine ekler
            await _basketService.SaveBasket(basketTotalDTO); // Sepeti kaydeder
            return Ok("Sepetteki Değişiklikler Kaydedildi..."); // Başarılı yanıt döner
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMyBasket() // Sepeti siler
        {
            var userID = _loginService.GetUserID; // Kullanıcının ID'sini alır
            await _basketService.DeleteBasket(userID); // Sepeti siler
            return Ok("Sepetiniz Silindi..."); // Başarılı yanıt döner
        }
    }
}
