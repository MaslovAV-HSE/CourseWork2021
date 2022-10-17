using ProductList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList.ViewModel
{
    public class ProductListViewModel
    {
        public static List<ProductModel> Products { get; set; } = new List<ProductModel>() { };
        
        public ProductListViewModel()
        {
            Products.Add(new ProductModel
            {
                Name = "Apelsin",
                Category = "Фрукты"
            });
            Products.Add(new ProductModel
            {

                Name = "Ananas",
                Category = "Фрукты"
            });
            Products.Add(new ProductModel
            {

                Name = "Банан",
                Category = "Фрукты"
            });
            Products.Add(new ProductModel
            {

                Name = "Виноград",
                Category = "Фрукты"
            });
            Products.Add(new ProductModel
            {

                Name = "Гранат",
                Category = "Фрукты"
            });
            Products.Add(new ProductModel
            {

                Name = "Дыня",
                Category = "Фрукты"
            });
        }
    }
}
