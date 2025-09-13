using MultiShop.DTOLayer.CatalogDTOs.FeatureDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureServices
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDTO>> GetAllFeatureAsync(); // Tüm özellikleri getirir
        Task CreateFeatureAsync(CreateFeatureDTO createFeatureDTO); // Yeni özellik oluşturur
        Task UpdateFeatureAsync(UpdateFeatureDTO updateFeatureDTO); // Kategoriyi günceller
        Task DeleteFeatureAsync(string id); // Özellik siler
        Task<UpdateFeatureDTO> GetByIDFeatureAsync(string id); // ID ile özellik getirir
    }
}
