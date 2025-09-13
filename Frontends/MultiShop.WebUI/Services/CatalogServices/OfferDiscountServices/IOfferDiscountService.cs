using MultiShop.DTOLayer.CatalogDTOs.OfferDiscountDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices
{
    public interface IOfferDiscountService
    {
        Task<List<ResultOfferDiscountDTO>> GetAllOfferDiscountAsync(); // Tüm kategorileri getirir
        Task CreateOfferDiscountAsync(CreateOfferDiscountDTO createOfferDiscountDTO); // Yeni kategori oluşturur
        Task UpdateOfferDiscountAsync(UpdateOfferDiscountDTO updateOfferDiscountDTO); // Kategoriyi günceller
        Task DeleteOfferDiscountAsync(string id); // Kategoriyi siler
        Task<UpdateOfferDiscountDTO> GetByIDOfferDiscountAsync(string id); // ID ile kategori getirir
    }
}
