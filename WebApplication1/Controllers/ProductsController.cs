using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TestDbContext _context;

        public ProductsController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();

        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Product>> GetProduct(string name)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
            
            if(product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { name = product.Name }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
