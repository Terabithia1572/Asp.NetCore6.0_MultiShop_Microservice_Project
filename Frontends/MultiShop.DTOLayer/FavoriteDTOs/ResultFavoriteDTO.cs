using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.FavoriteDTOs
{
    public class ResultFavoriteDTO
    {
        public int FavoriteID { get; set; }
        public string UserID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
