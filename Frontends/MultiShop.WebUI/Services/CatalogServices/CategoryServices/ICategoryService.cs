using MultiShop.DTOLayer.CatalogDTOs.CategoryDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDTO>> GetAllCategoryAsync(); // Tüm kategorileri getirir
        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO); // Yeni kategori oluşturur
        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO); // Kategoriyi günceller
        Task DeleteCategoryAsync(string id); // Kategoriyi siler
        Task<UpdateCategoryDTO> GetByIDCategoryAsync(string id); // ID ile kategori getirir
    }
}
