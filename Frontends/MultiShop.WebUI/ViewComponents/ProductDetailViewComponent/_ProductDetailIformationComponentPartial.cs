using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTOs;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponent
{
    public class _ProductDetailIformationComponentPartial: ViewComponent //bu sınıf ViewComponent sınıfından türetilir 
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        //public _ProductDetailIformationComponentPartial(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        private readonly IProductDetailService _productDetailService;

        public _ProductDetailIformationComponentPartial(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            ViewBag.v = id;
            //var client = _httpClientFactory.CreateClient();
            //var responseMessage = await client.GetAsync("https://localhost:1002/api/ProductDetails/GetProductDetailByProductID/" + id);
            //if (responseMessage.IsSuccessStatusCode)
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync();
            //    var values = JsonConvert.DeserializeObject<UpdateProductDetailDTO>(jsonData);
            //    return View(values);
            //}
            //return View();
            var values = await _productDetailService.GetByProductIDDetailAsync(id);
            return View(values);
        }
    }
}
