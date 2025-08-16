using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DTOLayer.CatalogDTOs.CategoryDTOs
{
    public class ResultCategoryDTO // ResultCategoryDTO sınıfı, kategori sonuçlarını temsil eder.
    {
        public string CategoryID { get; set; } // Kategori ID'sini tuttuk. 
        public string CategoryName { get; set; } // Kategori Adını Tuttuk.
        public string CategoryImageURL { get; set; } //Kategori Resim Görselini Tuttuk.
    }
}
