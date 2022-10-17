using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductList
{
    public class ProductListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductModel> Products { get; set; }
    }

}
