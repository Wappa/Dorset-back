using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Orders
    {
        [Key] 
        public int order_id { get; set; }
        
        public int customer_id { get; set; }
        
        public int products_id { get; set; }
    }
}