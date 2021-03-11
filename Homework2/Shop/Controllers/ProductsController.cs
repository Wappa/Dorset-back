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
    public class ProductsController : Controller
    {
        private readonly Context _context;

        public ProductsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProduct()
        {
            return await _context.products.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<AddProducts.AddProduct>> Add_Product(AddProducts.AddProduct productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Products()
            {
                product_id = productDTO.product_id,
                price = productDTO.price,
                product_name = productDTO.product_name
            };
            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetProduct", new {id = product.product_id}, productDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put_Values(int id, Products products)
        {
            if (id != products.product_id)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

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
            return _context.products.Any(e => e.product_id == id);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> Delete_values(int id)
        {
            var values = await _context.products.FindAsync(id);
            if (values == null)
            {
                return NotFound();
            }

            _context.products.Remove(values);
            await _context.SaveChangesAsync();

            return values;
        }
    }
}