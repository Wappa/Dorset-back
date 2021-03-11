using System.ComponentModel.DataAnnotations;

namespace Shop.Model
{
    public class Products
    {
        [Key] 
        public int product_id { get; set; }
        public string product_name { get; set; }
        public decimal price { get; set; }
    }
}
