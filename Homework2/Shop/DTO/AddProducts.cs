using System.ComponentModel.DataAnnotations;

namespace Shop.DTO
{
    public class AddProducts
    {
        public class AddProduct
        {
            [Required]
            public int product_id { get; set; }
            [Required]
            public string product_name { get; set; }
            [Required]
            public decimal price { get; set; }
        }
    }
}