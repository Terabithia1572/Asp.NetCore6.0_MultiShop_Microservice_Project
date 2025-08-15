using MultiShop.Catalog.DTOs.FeatureSliderDTOs;

namespace MultiShop.Catalog.Services.FeatureSliderServices
{
    public interface IFeatureSliderService
    {
        Task<List<ResultFeatureSliderDTO>> GetAllFeatureSliderAsync(); // Tüm kategorileri getirir
        Task CreateFeatureSliderAsync(CreateFeatureSliderDTO createFeatureSliderDTO); // Yeni kategori oluşturur
        Task UpdateFeatureSliderAsync(UpdateFeatureSliderDTO updateFeatureSliderDTO); // Kategoriyi günceller
        Task DeleteFeatureSliderAsync(string id); // Kategoriyi siler
        Task<GetByIDFeatureSliderDTO> GetByIDFeatureSliderAsync(string id); // ID ile kategori getirir
        Task FeatureSliderChangeStatusToTrue(string id); // Kategorinin durumunu true yapar
        Task FeatureSliderChangeStatusToFalse(string id); // Kategorinin durumunu false yapar
    }
}
