using MultiShop.DTOLayer.CatalogDTOs.OfferDiscountDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices
{
    public class OfferDiscountService: IOfferDiscountService
    {
        private readonly HttpClient _httpClient; //HttpClient nesnesi
        public OfferDiscountService(HttpClient httpClient) //Dependency Injection ile HttpClient nesnesi alınır
        {
            _httpClient = httpClient; //HttpClient nesnesi atanır
        }

        public async Task CreateOfferDiscountAsync(CreateOfferDiscountDTO createOfferDiscountDTO) //Yeni indirim kuponları oluşturur
        {
            await _httpClient.PostAsJsonAsync<CreateOfferDiscountDTO>("offerdiscounts", createOfferDiscountDTO); //HttpClient ile POST isteği gönderilir

        }

        public async Task DeleteOfferDiscountAsync(string id) //Kategoriyi siler
        {
            await _httpClient.DeleteAsync("offerdiscounts?id=" + id); //HttpClient ile DELETE isteği gönderilir
        }



        public async Task<List<ResultOfferDiscountDTO>> GetAllOfferDiscountAsync() //Tüm indirim kuponlarıleri getirir
        {

            var response = await _httpClient.GetAsync("offerdiscounts"); //HttpClient ile GET isteği gönderilir
            var jsonData = await response.Content.ReadAsStringAsync(); // JSON verisi okunur.
                                                                       //var values = await response.Content.ReadFromJsonAsync<List<ResultOfferDiscountDTO>>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDTO>>(jsonData); // JSON verisi DTO nesnesine dönüştürülür.
            return values; //Liste döndürülür

        }

        public async Task<UpdateOfferDiscountDTO> GetByIDOfferDiscountAsync(string id) //ID ile indirim kuponları getirir
        {
            var response = await _httpClient.GetAsync("offerdiscounts/" + id); //HttpClient ile GET isteği gönderilir
            var values = await response.Content.ReadFromJsonAsync<UpdateOfferDiscountDTO>(); //Gelen cevap JSON formatında okunur ve listeye dönüştürülür
            return values; //Liste döndürülür
        }

        public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDTO updateOfferDiscountDTO) //Kategoriyi günceller
        {
            await _httpClient.PutAsJsonAsync<UpdateOfferDiscountDTO>("offerdiscounts", updateOfferDiscountDTO); //HttpClient ile PUT isteği gönderilir

        }
    }
}
