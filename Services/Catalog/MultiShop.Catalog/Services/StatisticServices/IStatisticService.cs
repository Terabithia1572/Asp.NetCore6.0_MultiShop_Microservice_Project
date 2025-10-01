namespace MultiShop.Catalog.Services.StatisticServices
{
    public interface IStatisticService
    {
        Task<long> GetCategoryCount(); // Bize Kategori Sayısını Verecek
        Task<long> GetProduceCount(); // Ürün Sayısını vericek
        Task<long> GetBrandCount(); // Markaların sayısını vericek
        Task<decimal> GetProductAvgPrice(); // Toplam Ürün Fiyatını verecek
    }
}
