using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.CategoryDTOs;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs; // <-- ürün DTO'ları
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _CategoriesDefaultComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // 1) Kategorileri çek
            var catResp = await client.GetAsync("https://localhost:1002/api/Categories");
            if (!catResp.IsSuccessStatusCode)
                return View(new List<ResultCategoryDTO>());

            var categoriesJson = await catResp.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(categoriesJson)
                             ?? new List<ResultCategoryDTO>();

            // 2) Ürünleri çek (tek seferde)
            var prodResp = await client.GetAsync("https://localhost:1002/api/Products");
            if (prodResp.IsSuccessStatusCode)
            {
                var productsJson = await prodResp.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ResultProductDTO>>(productsJson)
                               ?? new List<ResultProductDTO>();

                // 3) Kategoriye göre saydır
                var counts = products
                    .Where(p => !string.IsNullOrEmpty(p.CategoryID))
                    .GroupBy(p => p.CategoryID)
                    .ToDictionary(g => g.Key, g => g.Count());

                // 4) Merge et
                foreach (var c in categories)
                    c.ProductCount = counts.TryGetValue(c.CategoryID, out var n) ? n : 0;
            }
            else
            {
                // Ürün servisi gelmezse 0 göster
                foreach (var c in categories) c.ProductCount = 0;
            }

            return View(categories);
        }
    }
}
