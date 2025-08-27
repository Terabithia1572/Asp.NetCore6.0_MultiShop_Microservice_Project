using AutoMapper;
using MongoDB.Driver.Core.Misc;
using MultiShop.Catalog.DTOs.AboutDTOs;
using MultiShop.Catalog.DTOs.BrandDTOs;
using MultiShop.Catalog.DTOs.CategoryDTOs;
using MultiShop.Catalog.DTOs.ContactDTOs;
using MultiShop.Catalog.DTOs.FeatureDTOs;
using MultiShop.Catalog.DTOs.FeatureSliderDTOs;
using MultiShop.Catalog.DTOs.OfferDiscountDTOs;
using MultiShop.Catalog.DTOs.ProductDetailDTOs;
using MultiShop.Catalog.DTOs.ProductDTOs;
using MultiShop.Catalog.DTOs.ProductImageDTOs;
using MultiShop.Catalog.DTOs.SpecialOfferDTOs;
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

            CreateMap<Product,ResultProductsWithCategoryDTO>().ReverseMap(); // Burada Product ile ResultProductsWithCategoryDTO arasında çift yönlü eşleme yapılıyor

            CreateMap<FeatureSlider, ResultFeatureSliderDTO>().ReverseMap(); // Burada FeatureSlider ile ResultFeatureSliderDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<FeatureSlider, CreateFeatureSliderDTO>().ReverseMap(); // Burada FeatureSlider ile CreateFeatureSliderDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<FeatureSlider, UpdateFeatureSliderDTO>().ReverseMap(); // Burada FeatureSlider ile UpdateFeatureSliderDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<FeatureSlider, GetByIDFeatureSliderDTO>().ReverseMap(); // Burada FeatureSlider ile GetByIDFeatureSliderDTO arasında çift yönlü eşleme yapılıyor

            CreateMap<SpecialOffer, ResultSpecialOfferDTO>().ReverseMap(); // Burada SpecialOffer ile ResultSpecialOfferDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<SpecialOffer, CreateSpecialOfferDTO>().ReverseMap(); // Burada SpecialOffer ile CreateSpecialOfferDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<SpecialOffer, UpdateSpecialOfferDTO>().ReverseMap(); // Burada SpecialOffer ile UpdateSpecialOfferDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<SpecialOffer, GetByIDSpecialOfferDTO>().ReverseMap(); // Burada SpecialOffer ile GetByIDSpecialOfferDTO arasında çift yönlü eşleme yapılıyor

            CreateMap<Entities.Feature, GetByIDFeatureDTO>().ReverseMap(); // Burada Feature ile GetByIDFeatureDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Entities.Feature, ResultFeatureDTO>().ReverseMap(); // Burada Feature ile ResultFeatureDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Entities.Feature, CreateFeatureDTO>().ReverseMap(); // Burada Feature ile CreateFeatureDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Entities.Feature, UpdateFeatureDTO>().ReverseMap(); // Burada Feature ile UpdateFeatureDTO arasında çift yönlü eşleme yapılıyor

            CreateMap<OfferDiscount, ResultOfferDiscountDTO>().ReverseMap(); // Burada OfferDiscount ile ResultOfferDiscountDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<OfferDiscount, CreateOfferDiscountDTO>().ReverseMap(); // Burada OfferDiscount ile CreateOfferDiscountDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<OfferDiscount, UpdateOfferDiscountDTO>().ReverseMap(); // Burada OfferDiscount ile UpdateOfferDiscountDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<OfferDiscount, GetByIDOfferDiscountDTO>().ReverseMap(); // Burada OfferDiscount ile GetByIDOfferDiscountDTO arasında çift yönlü eşleme yapılıyor

            CreateMap<Brand, ResultBrandDTO>().ReverseMap(); // Burada Brand ile ResultBrandDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Brand, CreateBrandDTO>().ReverseMap(); // Burada Brand ile CreateBrandDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Brand, UpdateBrandDTO>().ReverseMap(); // Burada Brand ile UpdateBrandDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Brand, GetByIDBrandDTO>().ReverseMap(); // Burada Brand ile GetByIDBrandDTO arasında çift yönlü eşleme yapılıyor

            CreateMap<About, ResultAboutDTO>().ReverseMap(); // Burada About ile ResultAboutDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<About, GetByIDAboutDTO>().ReverseMap(); // Burada About ile GetByIDAboutDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<About, CreateAboutDTO>().ReverseMap(); // Burada About ile CreateAboutDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<About, UpdateAboutDTO>().ReverseMap(); // Burada About ile UpdateAboutDTO arasında çift yönlü eşleme yapılıyor

            CreateMap<Contact, ResultContactDTO>().ReverseMap(); // Burada Contact ile ResultContactDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Contact, CreateContactDTO>().ReverseMap(); // Burada Contact ile CreateContactDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Contact, UpdateContactDTO>().ReverseMap(); // Burada Contact ile UpdateContactDTO arasında çift yönlü eşleme yapılıyor
            CreateMap<Contact, GetByIDContactDTO>().ReverseMap(); // Burada Contact ile GetByIDContactDTO arasında çift yönlü eşleme yapılıyor
        }
    }
}
