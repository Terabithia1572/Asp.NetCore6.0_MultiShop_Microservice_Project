using MultiShop.DTOLayer.CatalogDTOs.AboutDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDTO>> GetAllAboutAsync(); // Tüm kategorileri getirir
        Task CreateAboutAsync(CreateAboutDTO createAboutDTO); // Yeni kategori oluşturur
        Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO); // Kategoriyi günceller
        Task DeleteAboutAsync(string id); // Kategoriyi siler
        Task<UpdateAboutDTO> GetByIDAboutAsync(string id); // ID ile kategori getirir
    }
}
