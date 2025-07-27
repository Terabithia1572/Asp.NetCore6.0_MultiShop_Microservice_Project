namespace MultiShop.Discount.DTOs
{
    public class CreateCouponDTO
    {
        public string CouponCode { get; set; } //Kupon Kodunu tuttuk.
        public int CouponRate { get; set; } //Bu Kuponun İndirim oranını tuttuk.
        public bool CouponIsActive { get; set; } //Bu Kupon Aktif mi değil mi onu tuttuk.
        public DateTime CouponValidDate { get; set; } //Kupon geçerlilik tarihini tuttuk.
    }
}
