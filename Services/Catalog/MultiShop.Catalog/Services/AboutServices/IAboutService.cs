using MultiShop.Catalog.DTOs.AboutDTOs;

namespace MultiShop.Catalog.Services.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDTO>> GetAllAboutAsync(); // Tüm kategorileri getirir
        Task CreateAboutAsync(CreateAboutDTO createAboutDTO); // Yeni kategori oluşturur
        Task UpdateAboutAsync(UpdateAboutDTO updateAboutDTO); // Kategoriyi günceller
        Task DeleteAboutAsync(string id); // Kategoriyi siler
        Task<GetByIDAboutDTO> GetByIDAboutAsync(string id); // ID ile kategori getirir
    }
}
