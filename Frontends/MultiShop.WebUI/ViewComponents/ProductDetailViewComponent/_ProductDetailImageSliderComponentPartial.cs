using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;
using MultiShop.DTOLayer.CatalogDTOs.ProductImageDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailImageSliderComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _ProductDetailImageSliderComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return View(new GetByIDProductImageDTO()); // boş model gönder

            var client = _httpClientFactory.CreateClient();

            // FAZLA / KALDIRILDI
            var response = await client.GetAsync(
                $"https://localhost:1002/api/ProductImages/ProductImagesByProductID?id={id}");

            if (!response.IsSuccessStatusCode)
                return View(new GetByIDProductImageDTO()); // null gönderme

            var json = await response.Content.ReadAsStringAsync();

            // Eğer endpoint liste döndürüyorsa şu iki satırı değiştirin:
            // var list = JsonConvert.DeserializeObject<List<GetByIDProductImageDTO>>(json);
            // var value = list?.FirstOrDefault() ?? new GetByIDProductImageDTO();

            var value = JsonConvert.DeserializeObject<GetByIDProductImageDTO>(json)
                        ?? new GetByIDProductImageDTO(); // yine null olmasın

            return View(value);
        }
    }
}

