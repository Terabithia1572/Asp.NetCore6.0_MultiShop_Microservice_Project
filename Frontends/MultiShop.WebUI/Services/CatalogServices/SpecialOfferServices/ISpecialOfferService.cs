using MultiShop.DTOLayer.CatalogDTOs.SpecialOfferDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        Task<List<ResultSpecialOfferDTO>> GetAllSpecialOfferAsync(); // Tüm indirimleri getirir
        Task CreateSpecialOfferAsync(CreateSpecialOfferDTO createSpecialOfferDTO); // Yeni indirim oluşturur
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDTO updateSpecialOfferDTO); // İndirimi günceller
        Task DeleteSpecialOfferAsync(string id); // İndirimi siler
        Task<UpdateSpecialOfferDTO> GetByIDSpecialOfferAsync(string id); // ID ile indirim getirir
    }
}
