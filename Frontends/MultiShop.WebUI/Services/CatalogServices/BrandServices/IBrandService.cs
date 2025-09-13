using MultiShop.DTOLayer.CatalogDTOs.BrandDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.BrandServices
{
    public interface IBrandService
    {
        Task<List<ResultBrandDTO>> GetAllBrandAsync(); // Tüm kategorileri getirir
        Task CreateBrandAsync(CreateBrandDTO createBrandDTO); // Yeni kategori oluşturur
        Task UpdateBrandAsync(UpdateBrandDTO updateBrandDTO); // Kategoriyi günceller
        Task DeleteBrandAsync(string id); // Kategoriyi siler
        Task<UpdateBrandDTO> GetByIDBrandAsync(string id); // ID ile kategori getirir
    }
}
