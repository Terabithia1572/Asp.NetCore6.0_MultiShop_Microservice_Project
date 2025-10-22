using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CommentDTOs;
using MultiShop.WebUI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailReviewComponentPartial:ViewComponent //bu sınıf ViewComponent sınıfından türetilir 
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICommentService _commentService;

        public _ProductDetailReviewComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        //public _ProductDetailReviewComponentPartial(IHttpClientFactory httpClientFactory, ICommentService commentService)
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _commentService = commentService;
        //}


        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            //var client = _httpClientFactory.CreateClient();

            //var resp = await client.GetAsync($"https://localhost:7297/api/Comments/GetCommentsByProductId?productId={id}");
            //var list = new List<ResultCommentDTO>();

            //if (resp.IsSuccessStatusCode)
            //{
            //    var json = await resp.Content.ReadAsStringAsync();
            //    list = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(json) ?? new List<ResultCommentDTO>();
            //}

            // ViewData["ProductId"] = id; // id'yi View'e geçir
            //return View(list);          // asla null model dönme
            var values = await _commentService.GetCommentsByProductId(id);
            // 💡 Burada id’yi View’e geçiriyoruz
            ViewData["ProductID"] = id;
            return View(values);
        }
    }

}
