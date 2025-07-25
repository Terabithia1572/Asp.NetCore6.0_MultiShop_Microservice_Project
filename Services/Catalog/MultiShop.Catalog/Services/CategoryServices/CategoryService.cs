using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.CategoryDTOs;
using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection; // MongoDB'deki Category koleksiyonuna erişim sağlar.
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.

        public Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetByIDCategoryDTO> GetByIDCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
