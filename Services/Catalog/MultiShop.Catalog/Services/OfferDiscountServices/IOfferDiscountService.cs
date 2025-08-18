using MultiShop.Catalog.DTOs.OfferDiscountDTOs;

namespace MultiShop.Catalog.Services.OfferDiscountServices
{
    public interface IOfferDiscountService
    {
        Task<List<ResultOfferDiscountDTO>> GetAllOfferDiscountAsync(); // Tüm kategorileri getirir
        Task CreateOfferDiscountAsync(CreateOfferDiscountDTO createOfferDiscountDTO); // Yeni kategori oluşturur
        Task UpdateOfferDiscountAsync(UpdateOfferDiscountDTO updateOfferDiscountDTO); // Kategoriyi günceller
        Task DeleteOfferDiscountAsync(string id); // Kategoriyi siler
        Task<GetByIDOfferDiscountDTO> GetByIDOfferDiscountAsync(string id); // ID ile kategori getirir
    }
}
