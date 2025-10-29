// Areas/Admin/Models/OrderDetailWithImageVM.cs
namespace MultiShop.WebUI.Areas.Admin.Models
{
    public class OrderDetailWithImageVM
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }            // ProductQuantity/Amount normalize
        public decimal ProductTotalPrice { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
