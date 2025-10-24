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
        Task<List<ResultProductsWithCategoryDTO>> GetProductsWithCategoryAsync(); // Kategorilerle birlikte Ürünleri getirir
        Task<List<ResultProductsWithCategoryDTO>> GetProductsWithByCategoryByCategoryIDAsync(string categoryID); // Belirli bir kategori ile Ürünleri getirir

        // 🔥 Yeni metod (ürünleri indirim oranlarıyla birlikte getirir)
        // 🆕 EKLENDİ: İndirimli ürün listesini getir
        Task<List<ResultProductWithDiscountDTO>> GetAllProductWithDiscountAsync();

        // 🆕 EKLENDİ: Tüm ürünleri kategorileriyle birlikte getir (All sayfası için)
        Task<List<ResultProductsWithCategoryDTO>> GetAllProductsWithCategoryAsync();

    }
}
