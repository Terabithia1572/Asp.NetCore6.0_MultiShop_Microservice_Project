namespace MultiShop.Basket.DTOs.BasketItemDTOs
{
    public class BasketItemDTO
    {
        public string ProductID { get; set; } //Ürün ID'si
        public string ProductName { get; set; } //Ürün Adı
        public int ProductQuantity { get; set; } //Ürün Miktarı
        public decimal ProductPrice { get; set; } //Ürün Fiyatı
    }
}
