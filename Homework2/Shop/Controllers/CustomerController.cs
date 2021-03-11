using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.DTO;
using Shop.Model;

namespace Shop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly Context _context;

        public CustomerController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _context.customer.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomer_byId(int id)
        {
                var order = from orders in _context.orders
                join product in _context.products on orders.products_id equals product.product_id
                join customer in _context.customer on orders.customer_id equals customer.customer_id
                select new OrderDTO
                {
                    customer_id = customer.customer_id,
                    price = product.price,
                    order_id = orders.order_id,
                    product_id = product.product_id
                };

            var customers = from customer in _context.customer
                join orders in _context.orders on customer.customer_id equals orders.customer_id
                select new CustomerOrderListDTO()
                {
                    Customer_id = customer.customer_id,
                    Name = customer.customer_name,
                    Orders = order.Where(x => x.customer_id == customer.customer_id).ToList()
                };

            var customer_by_id = customers.ToList().Find(x => x.Customer_id == id);

            if (customer_by_id == null)
            {
                return NotFound();
            }
            return customer_by_id;
        }
        
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.customer.Add(customer);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetCustomer", new { id = customer.customer_id }, customer);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put_Values(int id, Customer customer)
        {
            if (id != customer.customer_id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

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
            return _context.customer.Any(e => e.customer_id == id);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete_values(int id)
        {
            var values = await _context.customer.FindAsync(id);
            if (values == null)
            {
                return NotFound();
            }

            _context.customer.Remove(values);
            await _context.SaveChangesAsync();

            return values;
        }
    }
}