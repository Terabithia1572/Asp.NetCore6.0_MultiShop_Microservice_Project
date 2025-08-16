using MultiShop.Catalog.DTOs.FeatureDTOs;

namespace MultiShop.Catalog.Services.FeatureServices
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDTO>> GetAllFeatureAsync(); // Tüm özellikleri getirir
        Task CreateFeatureAsync(CreateFeatureDTO createFeatureDTO); // Yeni özellik oluşturur
        Task UpdateFeatureAsync(UpdateFeatureDTO updateFeatureDTO); // Kategoriyi günceller
        Task DeleteFeatureAsync(string id); // Özellik siler
        Task<GetByIDFeatureDTO> GetByIDFeatureAsync(string id); // ID ile özellik getirir
    }
}
