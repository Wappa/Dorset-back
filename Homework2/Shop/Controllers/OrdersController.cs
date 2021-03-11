using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.DTO;
using Shop.Models;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly Context _context;

        public OrdersController(Context context)
        {
            _context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> GetOrder()
        {
            var order = from orders in _context.orders 
                join product in _context.products on orders.products_id equals product.product_id
                select new OrderDTO()
                {
                    customer_id = orders.customer_id,
                    order_id = orders.order_id,
                    product_id = product.product_id,
                    price = product.price
                };

            return Ok(order);
        }
        
        [HttpGet("{id}")]
        public ActionResult<OrderDTO> GetOrder_byId(int id)
        {
            var order = from orders in _context.orders
                join product in _context.products on orders.products_id equals product.product_id
                join customer in _context.customer on orders.customer_id equals customer.customer_id
                select new OrderDTO
                {
                    customer_id = customer.customer_id,
                    order_id = orders.order_id,
                    product_id = product.product_id,
                    price = product.price
                };

            var order_by_id = order.ToList().Find(x => x.order_id == id);

            if (order_by_id == null)
            {
                return NotFound();
            }
            return order_by_id;
        }
        
        [HttpPost]
        public async Task<ActionResult<Orders>> PostOrders(Orders order)
        {
            _context.orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetOrder", new { id = order.order_id }, order);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put_Values(int id, Orders orders)
        {
            if (id != orders.order_id)
            {
                return BadRequest();
            }

            _context.Entry(orders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValuesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ValuesExists(int id)
        {
            return _context.orders.Any(e => e.order_id == id);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders>> Delete_orders(int id)
        {
            var values = await _context.orders.FindAsync(id);
            if (values == null)
            {
                return NotFound();
            }

            _context.orders.Remove(values);
            await _context.SaveChangesAsync();

            return values;
        }
    }
}