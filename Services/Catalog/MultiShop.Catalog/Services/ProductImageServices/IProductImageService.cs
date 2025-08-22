using MultiShop.Catalog.DTOs.ProductImageDTOs;

namespace MultiShop.Catalog.Services.ProductImageServices
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
