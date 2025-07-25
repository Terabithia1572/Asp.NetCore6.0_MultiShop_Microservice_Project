﻿using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.DTOs.ProductDTOs;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper; // AutoMapper kullanarak DTO'lar ve Entity'ler arasında dönüşüm yapar. AutoMapper kütüphanesi ile nesneler arası otomatik dönüşüm işlemlerinde kullanılır.
        private readonly IMongoCollection<Product> _productCollection; // MongoDB'deki Product koleksiyonuna erişim sağlar.

        public ProductService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // MongoDB ile bağlantı kurmak için istemci oluşturuluyor.
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // Bağlantı kurulan MongoDB'de kullanılacak veritabanı seçiliyor.
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName); // Product koleksiyonu üzerinden işlem yapabilmek için referans alınıyor.
            _mapper = mapper; // AutoMapper örneği sınıfın _mapper alanına atanıyor.
        }

        public async Task CreateProductAsync(CreateProductDTO createProductDTO)
        {
            var values=_mapper.Map<Product>(createProductDTO); // createProductDTO nesnesindeki verileri, Product tipindeki bir nesneye dönüştürür (mapler).
            await _productCollection.InsertOneAsync(values); // Dönüştürülen Product nesnesini, MongoDB'deki Product koleksiyonuna asenkron olarak ekler.
        }

        public async Task DeleteProductAsync(string id)
        {
           await _productCollection.DeleteOneAsync(x => x.ProductID == id); // Verilen id'ye sahip olan Product nesnesini MongoDB'deki Product koleksiyonundan asenkron olarak siler.

        }

        public async Task<List<ResultProductDTO>> GetAllProductAsync()
        {
            var values=await _productCollection.Find(x => true).ToListAsync(); // MongoDB'deki Product koleksiyonundan tüm Product nesnelerini asenkron olarak alır.
            return _mapper.Map<List<ResultProductDTO>>(values); // Alınan Product nesnelerini ResultProductDTO tipine dönüştürür ve döner.
        }

        public async Task<GetByIDProductDTO> GetByIDProductAsync(string id)
        {
            var values=await _productCollection.Find<Product>(x => x.ProductID == id).FirstOrDefaultAsync(); // Verilen id'ye sahip ilk Product nesnesini, MongoDB'deki Product koleksiyonundan asenkron olarak bulur (yoksa null döner).
            return _mapper.Map<GetByIDProductDTO>(values); // Bulunan Product nesnesini GetByIDProductDTO tipine mapleyip (dönüştürüp) döner.

        }

        public async Task UpdateProductAsync(UpdateProductDTO updateProductDTO)
        {
            var values = _mapper.Map<Product>(updateProductDTO); // updateProductDTO nesnesindeki verileri, Product tipindeki yeni bir nesneye dönüştürür.
            await _productCollection.FindOneAndReplaceAsync(
                x => x.ProductID == updateProductDTO.ProductID,
                values); // Verilen ProductID'ye sahip olan Product nesnesini MongoDB'deki Product koleksiyonunda bulur ve bulduğu nesneyi tamamen values ile değiştirerek asenkron olarak günceller (tüm alanları yeni değerlerle değiştirir).
        }
    }
}
