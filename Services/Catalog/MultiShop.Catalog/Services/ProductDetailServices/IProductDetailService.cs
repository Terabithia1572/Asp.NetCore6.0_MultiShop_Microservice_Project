using MultiShop.Catalog.DTOs.ProductDetailDTOs;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
    public interface IProductDetailService
    {
        Task<List<ResultProductDetailDTO>> GetAllProductDetailAsync(); // Tüm ürünlerin detaylarını asenkron olarak getirir.
        Task CreateProductDetailAsync(CreateProductDetailDTO createProductDetailDTO); // Yeni bir ürün detayı oluşturur ve MongoDB'ye ekler (asenkron).
        Task UpdateProductDetailAsync(UpdateProductDetailDTO updateProductDetailDTO); // Verilen güncelleme bilgileriyle, ilgili ürün detayını asenkron olarak günceller.
        Task DeleteProductDetailAsync(string id); // Verilen id'ye sahip ürün detayını MongoDB'den asenkron olarak siler.
        Task<GetByIDProductDetailDTO> GetByIDProductDetailAsync(string id); // Belirtilen id'ye sahip ürün detayını asenkron olarak bulur ve getirir.
        Task<GetByIDProductDetailDTO> GetByProductIDDetailAsync(string id); // Belirtilen id'ye sahip ürün detayını asenkron olarak bulur ve getirir.
    }
}
