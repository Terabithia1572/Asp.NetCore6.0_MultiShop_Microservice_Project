using MultiShop.Catalog.DTOs.SpecialOfferDTOs;

namespace MultiShop.Catalog.Services.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        Task<List<ResultSpecialOfferDTO>> GetAllSpecialOfferAsync(); // Tüm indirimleri getirir
        Task CreateSpecialOfferAsync(CreateSpecialOfferDTO createSpecialOfferDTO); // Yeni indirim oluşturur
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDTO updateSpecialOfferDTO); // İndirimi günceller
        Task DeleteSpecialOfferAsync(string id); // İndirimi siler
        Task<GetByIDSpecialOfferDTO> GetByIDSpecialOfferAsync(string id); // ID ile indirim getirir
    }
}
