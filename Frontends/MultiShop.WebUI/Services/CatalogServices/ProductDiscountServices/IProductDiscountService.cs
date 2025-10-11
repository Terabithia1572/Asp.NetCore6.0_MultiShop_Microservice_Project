using MultiShop.DTOLayer.CatalogDTOs.ProductDiscountDTOs;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDiscountServices
{
    public interface IProductDiscountService
    {
        Task<List<ResultProductDiscountDTO>> GetAllProductDiscountAsync();
        Task<GetByIDProductDiscountDTO> GetByIdProductDiscountAsync(string id);
        Task CreateProductDiscountAsync(CreateProductDiscountDTO dto);
        Task UpdateProductDiscountAsync(UpdateProductDiscountDTO dto);
        Task DeleteProductDiscountAsync(string id);
    }
}
