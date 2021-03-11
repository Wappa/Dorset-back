namespace Shop.DTO
{
    public class OrderDTO
    {
        public int order_id { get; set; }
        
        public int customer_id { get; set; }
        
        public int product_id { get; set; }

        public decimal price { get; set; }
    }
}