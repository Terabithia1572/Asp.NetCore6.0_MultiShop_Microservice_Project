namespace MultiShop.Catalog.Services.StatisticServices
{
    public interface IStatisticService
    {
        long GetCategoryCount(); // Bize Kategori Sayısını Verecek
        long GetProduceCount(); // Ürün Sayısını vericek
        long GetBrandsCount(); // Markaların sayısını vericek
        decimal GetProductAvgPrice(); // Toplam Ürün Fiyatını verecek
    }
}
