using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.SpecialOfferDTOs;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
  //  [AllowAnonymous]
    [Area("Admin")] // Bu Area'nın adı "Admin" olarak ayarlanır. yani URL'de /Admin/ ile başlayacak.
    [Route("Admin/[controller]/[action]")] // Bu controller için rota ayarlanır. Örneğin: /Admin/SpecialOffer/Index
    public class SpecialOfferController : Controller
    {
        //private readonly IHttpClientFactory _httpClientFactory;

        //public SpecialOfferController(IHttpClientFactory httpClientFactory)
        //{
        //    _httpClientFactory = httpClientFactory;
        //}
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOfferController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        public async Task<IActionResult> Index()
        {
            SpecialOfferViewBagList(); // Bu ViewBag'ler, view içinde kullanılacak verileri taşır.
            var values = await _specialOfferService.GetAllSpecialOfferAsync(); // Tüm özel teklifler listesini alır.
            return View(values); // Listeyi view'e gönderir.

        }
        [HttpGet]
        public IActionResult CreateSpecialOffer()
        {
            SpecialOfferViewBagList();
            return View(); // İndirim ekleme sayfası için view döndürülür.
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDTO createSpecialOfferDTO)
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDTO); // Yeni indirim oluşturur.
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" }); // İndirim listesine yönlendirilir.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id); // İndirimi siler.
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" }); // İndirim listesine yönlendirilir.
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            SpecialOfferViewBagList();
            var values = await _specialOfferService.GetByIDSpecialOfferAsync(id); // ID ile indirimi getirir.
            return View(values); // İndirim güncelleme sayfası için view döndürülür.
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDTO updateSpecialOfferDTO)
        {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDTO); // İndirimi günceller.
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" }); // İndirim listesine yönlendirilir.
        }
        void SpecialOfferViewBagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "İndirimler";
            ViewBag.v3 = "İndirim Listesi";
            ViewBag.v4 = "İndirim İşlemleri";
        }
    }
}
