using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.CategoryDTOs;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;

        public _CategoriesDefaultComponentPartial(IHttpClientFactory httpClientFactory, ICategoryService categoryService)
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // 🌐 1️⃣ Kategorileri public endpoint’ten çekiyoruz
            // [AllowAnonymous] olduğu için artık login gerekmiyor.
            var catResp = await client.GetAsync("http://localhost:1002/api/Categories");
            if (!catResp.IsSuccessStatusCode)
                return View(new List<ResultCategoryDTO>()); // Eğer hata dönerse boş liste

            var categoriesJson = await catResp.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(categoriesJson)
                             ?? new List<ResultCategoryDTO>();

            // 🌐 2️⃣ Ürünleri public endpoint’ten çekiyoruz
            var prodResp = await client.GetAsync("http://localhost:1002/api/Products");
            if (prodResp.IsSuccessStatusCode)
            {
                var productsJson = await prodResp.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ResultProductDTO>>(productsJson)
                               ?? new List<ResultProductDTO>();

                // 🧮 3️⃣ Kategorilere göre ürün sayısını hesapla
                var counts = products
                    .Where(p => !string.IsNullOrEmpty(p.CategoryID))
                    .GroupBy(p => p.CategoryID)
                    .ToDictionary(g => g.Key, g => g.Count());

                // 🔄 4️⃣ Ürün sayılarını kategori listesine ekle
                foreach (var c in categories)
                    c.ProductCount = counts.TryGetValue(c.CategoryID, out var n) ? n : 0;
            }
            else
            {
                // 💤 Ürün servisi gelmezse 0 göster
                foreach (var c in categories)
                    c.ProductCount = 0;
            }

            // ✅ 5️⃣ View’a kategorileri gönder
            return View(categories);
        }
    }
}
