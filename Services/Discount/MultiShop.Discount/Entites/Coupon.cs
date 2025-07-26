namespace MultiShop.Discount.Entites
{
    public class Coupon
    { 
        public int CouponID { get; set; } //Kupon ID 'sini tuttuk.
        public string CouponCode { get; set; } //Kupon Kodunu tuttuk.
        public int CountRate { get; set; } //Bu Kuponun İndirim oranını tuttuk.
        public bool CouponIsActive { get; set; } //Bu Kupon Aktif mi değil mi onu tuttuk.
        public DateTime CouponValidDate { get; set; } //Kupon geçerlilik tarihini tuttuk.
    }
}
