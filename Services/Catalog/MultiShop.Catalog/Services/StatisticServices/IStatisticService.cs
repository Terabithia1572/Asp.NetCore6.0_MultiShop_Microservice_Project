namespace MultiShop.Catalog.Services.StatisticServices
{
    public interface IStatisticService
    {
        int GetCategoryCount(); // Bize Kategori Sayısını Verecek
        int GetProduceCount(); // Ürün Sayısını vericek
        int GetBrandsCount(); // Markaların sayısını vericek
        decimal GetProductAvgPrice(); // Toplam Ürün Fiyatını verecek
    }
}
