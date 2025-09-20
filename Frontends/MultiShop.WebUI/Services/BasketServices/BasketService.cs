using MultiShop.DTOLayer.BasketDTOs;

namespace MultiShop.WebUI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddBasketItem(BasketItemDTO basketItemDTO)
        {
           var values=await GetBasket(); // mevcut sepeti al
            if (values != null) // eğer sepet boş değilse
            {
                if(!values.BasketItems.Any(x=>x.ProductID==basketItemDTO.ProductID)) // eğer sepette aynı ürün yoksa
                {
                    values.BasketItems.Add(basketItemDTO); // yeni ürünü sepete ekle
                }
                else
                {
                    values=new BasketTotalDTO(); // yeni bir sepet oluştur
                    values.BasketItems.Add(basketItemDTO); // ürünü yeni sepete ekle
                }
            }
            await SaveBasket(values); // güncellenmiş sepeti kaydet
        }

        public Task DeleteBasket(string UserID)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketTotalDTO> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("baskets"); // "baskets" endpoint'ine GET isteği gönder
            var values=await responseMessage.Content.ReadFromJsonAsync<BasketTotalDTO>(); // gelen cevabı BasketTotalDTO türüne dönüştür
            return values; // dönüştürülen veriyi döndür
        }

        public async Task<bool> RemoveBasketItem(string ProductID)
        {
            var values=await GetBasket(); // mevcut sepeti al
            var deletedItem=values.BasketItems.FirstOrDefault(x=>x.ProductID==ProductID); // silinecek ürünü bul
            var result=values.BasketItems.Remove(deletedItem); // ürünü sepetten çıkar
            await SaveBasket(values); // güncellenmiş sepeti kaydet
            return true; // işlemin başarılı olduğunu döndür

        }

        public async Task SaveBasket(BasketTotalDTO basketTotalDTO)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDTO>("baskets", basketTotalDTO); // "baskets" endpoint'ine POST isteği gönder ve basketTotalDTO'yu JSON olarak ekle
        }
    }
}
