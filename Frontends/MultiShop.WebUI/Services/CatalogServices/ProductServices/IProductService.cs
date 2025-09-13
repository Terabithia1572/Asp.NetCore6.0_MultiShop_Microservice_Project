using MultiShop.DTOLayer.CatalogDTOs.ProductDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDTO>> GetAllProductAsync(); // Tüm Ürünleri getirir
        Task CreateProductAsync(CreateProductDTO createProductDTO); // Yeni Ürün oluşturur
        Task UpdateProductAsync(UpdateProductDTO updateProductDTO); // Ürünyi günceller
        Task DeleteProductAsync(string id); // Ürünyi siler
        Task<UpdateProductDTO> GetByIDProductAsync(string id); // ID ile Ürün getirir
        Task<List<ResultProductWithCategoryDTO>> GetProductsWithCategoryAsync(); // Kategorilerle birlikte Ürünleri getirir
        Task<List<ResultProductWithCategoryDTO>> GetProductsWithByCategoryByCategoryIDAsync(string categoryID); // Belirli bir kategori ile Ürünleri getirir
    }
}
