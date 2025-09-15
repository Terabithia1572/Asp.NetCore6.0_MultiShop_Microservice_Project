using MultiShop.DTOLayer.CatalogDTOs.ProductImageDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices
{
    public interface IProductImageService
    {
        Task<List<ResultProductImageDTO>> GetAllProductImageAsync(); // Tüm Resim leri getirir
        Task CreateProductImageAsync(CreateProductImageDTO createProductImageDTO); // Yeni Resim  oluşturur
        Task UpdateProductImageAsync(UpdateProductImageDTO updateProductImageDTO); // Resim yi günceller
        Task DeleteProductImageAsync(string id); // Resim yi siler
        Task<GetByIDProductImageDTO> GetByIDProductImageAsync(string id); // ID ile Resim  getirir
        Task<GetByIDProductImageDTO> GetByProductIDProductImageAsync(string productId); // Ürün ID ile Resim  getirir
    }
}
