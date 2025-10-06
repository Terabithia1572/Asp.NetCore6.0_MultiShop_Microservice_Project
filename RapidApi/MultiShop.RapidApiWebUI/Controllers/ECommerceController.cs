using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class ECommerceController : Controller
    {
        public async Task< IActionResult> ECommerceList()
        {
           
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-product-search.p.rapidapi.com/search-light-v2?q=Logitech%20f310&country=tr&language=en&page=1&limit=10&sort_by=BEST_MATCH&product_condition=ANY&return_filters=false"),
                Headers =
    {
        { "x-rapidapi-key", "" },
        { "x-rapidapi-host", "real-time-product-search.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ECommerceViewModel.Rootobject>>(body);
                return View(values);
            }
           
        }
    }
}
