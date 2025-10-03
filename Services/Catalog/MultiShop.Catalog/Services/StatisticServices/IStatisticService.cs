namespace MultiShop.Catalog.Services.StatisticServices
{
    public interface IStatisticService
    {
        Task<long> GetCategoryCount(); // Bize Kategori Sayısını Verecek
        Task<long> GetProductCount(); // Ürün Sayısını vericek
        Task<long> GetBrandCount(); // Markaların sayısını vericek
        Task<decimal> GetProductAvgPrice(); // Toplam Ürün Fiyatını verecek
        Task<string> GetMaximumPriceProductName(); // En yüksek fiyatlı ürünün adını getirecek
        Task<string> GetMinimumPriceProductName(); // En düşük fiyatlı ürünün adını getirecek
    }
}
