using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.AboutDTOs;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutComponentPartial:ViewComponent
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public _FooterUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}

        private readonly IAboutService _aboutService;

        public _FooterUILayoutComponentPartial(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task< IViewComponentResult> InvokeAsync() //bu metot, bu view component çağrıldığında çalışır.
        {

            //string token = ""; // 

            //using (var httpClient = new HttpClient())
            //{
            //    var request = new HttpRequestMessage
            //    {
            //        RequestUri = new Uri("http://localhost:5001/connect/token"),
            //        Method = HttpMethod.Post,
            //        Content = new FormUrlEncodedContent(new Dictionary<string, string>
            //        {
            //            {"client_id","MultiShopVisitorID"},
            //            {"client_secret","multishopsecret"},
            //            {"grant_type","client_credentials"}
            //            //{"scope","ResourceCatalog"}
            //        })
            //    };
            //    using (var response = await httpClient.SendAsync(request))
            //    {
            //        if (response.IsSuccessStatusCode)
            //        {
            //            var jsonResult = await response.Content.ReadAsStringAsync();
            //            var tokenResponse = JObject.Parse(jsonResult);
            //            token = tokenResponse["access_token"].ToString();

            //        }
            //    }
            //}

            //var client = _httpClientFactory.CreateClient(); // IHttpClientFactory kullanarak HttpClient oluşturulur.
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //var responseMessage = await client.GetAsync("https://localhost:1002/api/Abouts"); // API'den kategori verilerini almak için GET isteği yapılır.
            //if (responseMessage.IsSuccessStatusCode) // Eğer istek başarılıysa
            //{
            //    var jsonData = await responseMessage.Content.ReadAsStringAsync(); // JSON verisi okunur.
            //    var values = JsonConvert.DeserializeObject<List<ResultAboutDTO>>(jsonData); // JSON verisi dinamik bir listeye dönüştürülür.
            //    return View(values); // Dönüştürülen liste view'e gönderilir.
            //}
            //return View();

            var values = await _aboutService.GetAllAboutAsync();
            return View(values);
        }
    }
}
