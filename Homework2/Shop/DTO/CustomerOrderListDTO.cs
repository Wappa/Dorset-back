using System.Collections.Generic;

namespace Shop.DTO
{
    public class CustomerOrderListDTO : CustomerDTO
    {
        public List<OrderDTO> Orders { get; set; }

    }
}