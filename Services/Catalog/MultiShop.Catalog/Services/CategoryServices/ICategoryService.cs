using MultiShop.Catalog.DTOs.CategoryDTOs;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDTO>> GetAllCategoryAsync(); // Tüm kategorileri getirir
        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO); // Yeni kategori oluşturur
        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO); // Kategoriyi günceller
        Task DeleteCategoryAsync(string id); // Kategoriyi siler
        Task<GetByIDCategoryDTO> GetByIDCategoryAsync(string id); // ID ile kategori getirir

    }
}
