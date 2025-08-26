using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CommentDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailReviewComponentPartial:ViewComponent //bu sınıf ViewComponent sınıfından türetilir 
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailReviewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var resp = await client.GetAsync($"https://localhost:7297/api/Comments/GetCommentsByProductId?productId={id}");
            var list = new List<ResultCommentDTO>();

            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(json) ?? new List<ResultCommentDTO>();
            }

            ViewData["ProductId"] = id; // id'yi View'e geçir
            return View(list);          // asla null model dönme
        }
    }

}
