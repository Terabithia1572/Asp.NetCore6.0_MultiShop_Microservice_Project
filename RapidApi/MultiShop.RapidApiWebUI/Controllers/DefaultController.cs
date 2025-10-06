using Microsoft.AspNetCore.Mvc;
using MultiShop.RapidApiWebUI.Models;
using Newtonsoft.Json;

namespace MultiShop.RapidApiWebUI.Controllers
{
    public class DefaultController : Controller
    {
        public async Task< IActionResult> WeatherDetail()
        {
            var client = new HttpClient(); // 
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://the-weather-api.p.rapidapi.com/api/weather/Van"),
                Headers =
    {
        { "x-rapidapi-key", "" },
        { "x-rapidapi-host", "the-weather-api.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
               
                var values=JsonConvert.DeserializeObject<WeatherViewModel.Rootobject>(body);
                ViewBag.cityTemp = values.data.temp;
                return View();
            }    
        }
        public async Task<IActionResult> Exchange()
        {
        
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://real-time-finance-data.p.rapidapi.com/company-cash-flow?symbol=AAPL%3ANASDAQ&period=QUARTERLY&language=en"),
                Headers =
    {
        { "x-rapidapi-key", "" },
        { "x-rapidapi-host", "real-time-finance-data.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ExchangeViewModel.Rootobject>(body);
                ViewBag.Exchange=values.data.exchange_rate;
                return View();
            }
        }
    }
}
