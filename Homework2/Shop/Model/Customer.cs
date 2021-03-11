using System.ComponentModel.DataAnnotations;

namespace Shop.Model
{
    public class Customer
    {
        [Key] 
        public int customer_id { get; set; }

        public string customer_name { get; set; }
    }
}