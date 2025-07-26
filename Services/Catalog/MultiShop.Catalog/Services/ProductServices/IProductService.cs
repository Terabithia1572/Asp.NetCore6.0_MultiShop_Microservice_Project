using MultiShop.Catalog.DTOs.ProductDTOs;

namespace MultiShop.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDTO>> GetAllProductAsync(); // Tüm Ürünleri getirir
        Task CreateProductAsync(CreateProductDTO createProductDTO); // Yeni Ürün oluşturur
        Task UpdateProductAsync(UpdateProductDTO updateProductDTO); // Ürünyi günceller
        Task DeleteProductAsync(string id); // Ürünyi siler
        Task<GetByIDProductDTO> GetByIDProductAsync(string id); // ID ile Ürün getirir
    }
}
