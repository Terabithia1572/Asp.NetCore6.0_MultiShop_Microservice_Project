using AutoMapper;
using MultiShop.Catalog.DTOs.CategoryDTOs;
using MultiShop.Catalog.DTOs.ProductDetailDTOs;
using MultiShop.Catalog.DTOs.ProductDTOs;
using MultiShop.Catalog.DTOs.ProductImageDTOs;
using MultiShop.Catalog.Entities;

namespace MultiShop.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            // Eşleme yapılandırmalarınızı buraya ekleyin
            // Örneğin:
            // CreateMap<SourceType, DestinationType>();

            CreateMap<Category, ResultCategoryDTO>().ReverseMap(); //Burada Kategori ile ResultCategoryDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Category, CreateCategoryDTO>().ReverseMap(); // Burada Kategori ile CreateCategoryDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap(); // Burada Kategori ile UpdateCategoryDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Category, GetByIDCategoryDTO>().ReverseMap(); // Burada Kategori ile GetByIDCategoryDTO arasında çift yönlü eşleme yapılıyor


            CreateMap<Product,ResultProductDTO>().ReverseMap(); // Burada Product ile ResultProductDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Product, CreateProductDTO>().ReverseMap(); // Burada Product ile CreateProductDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Product, UpdateProductDTO>().ReverseMap(); // Burada Product ile UpdateProductDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Product, GetByIDProductDTO>().ReverseMap(); // Burada Product ile GetByIDProductDTO arasında çift yönlü eşleme yapılıyor


            CreateMap<ProductDetail, ResultProductDetailDTO>().ReverseMap(); // Burada ProductDetail ile ResultProductDetailDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<ProductDetail, CreateProductDetailDTO>().ReverseMap(); // Burada ProductDetail ile CreateProductDetailDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<ProductDetail, UpdateProductDetailDTO>().ReverseMap(); // Burada ProductDetail ile UpdateProductDetailDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<ProductDetail, GetByIDProductDetailDTO>().ReverseMap(); // Burada ProductDetail ile GetByIDProductDetailDTO arasında çift yönlü eşleme yapılıyor

            CreateMap<ProductImage, ResultProductImageDTO>().ReverseMap(); // Burada ProductImage ile ResultProductImageDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<ProductImage, CreateProductImageDTO>().ReverseMap(); // Burada ProductImage ile CreateProductImageDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<ProductImage, UpdateProductImageDTO>().ReverseMap(); // Burada ProductImage ile UpdateProductImageDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<ProductImage, GetByIDProductImageDTO>().ReverseMap(); // Burada ProductImage ile GetByIDProductImageDTO arasında çift yönlü eşleme yapılıyor
        }
    }
}
