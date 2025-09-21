using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.DiscountDTOs
{
    public class GetDiscountCodeDetailByCode
    {
        public int CouponID { get; set; } //Kupon ID 'sini tuttuk.
        public string CouponCode { get; set; } //Kupon Kodunu tuttuk.
        public int CouponRate { get; set; } //Bu Kuponun İndirim oranını tuttuk.
        public bool CouponIsActive { get; set; } //Bu Kupon Aktif mi değil mi onu tuttuk.
        public DateTime CouponValidDate { get; set; } //Kupon geçerlilik tarihini tuttuk.
    }
}
