using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketServices;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents
{
    public class _ShoppingCartProductListComponentPartial: ViewComponent //bu sınıf ViewComponent sınıfından türetilmiştir
    {
        private readonly IBasketService _basketService; // Sepet servisi için bir alan tanımlanır

        public _ShoppingCartProductListComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task< IViewComponentResult> InvokeAsync() //Invoke metodu, ViewComponent'ın çağrıldığında ne yapacağını belirler
        {
            //var values = await _basketService.GetBasket(); // Sepeti getir
            //return View(values); // Sepet görünümünü döndür

            //return View(); //View() metodu, varsayılan olarak "_ShoppingCartProductListComponentPartial.cshtml" dosyasını render eder
            var basketTotal=await _basketService.GetBasket();
            var basketItems = basketTotal.BasketItems;
            return View(basketItems); // Sepet ürünlerini görünümde kullanmak üzere döndür
        }
    }
}
