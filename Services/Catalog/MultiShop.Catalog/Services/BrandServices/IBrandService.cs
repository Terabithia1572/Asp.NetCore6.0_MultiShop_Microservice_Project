using MultiShop.Catalog.DTOs.BrandDTOs;

namespace MultiShop.Catalog.Services.BrandServices
{
    public interface IBrandService
    {
        Task<List<ResultBrandDTO>> GetAllBrandAsync(); // Tüm kategorileri getirir
        Task CreateBrandAsync(CreateBrandDTO createBrandDTO); // Yeni kategori oluşturur
        Task UpdateBrandAsync(UpdateBrandDTO updateBrandDTO); // Kategoriyi günceller
        Task DeleteBrandAsync(string id); // Kategoriyi siler
        Task<GetByIDBrandDTO> GetByIDBrandAsync(string id); // ID ile kategori getirir
    }
}
