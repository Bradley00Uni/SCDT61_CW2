using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopDeliveryAPI.Data;
using OnlineShopDeliveryAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopDeliveryAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            return await _context.Products.Include("Order").ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(int id)
        {
            var ProductModel = await _context.Products.Include("Order").Where(x => x.ProductId == id).ToListAsync();

            if (ProductModel == null)
            {
                return NotFound();
            }

            return ProductModel.FirstOrDefault();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(int id, ProductModel ProductModel)
        {
            if (id != ProductModel.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(ProductModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
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

        [HttpPost]
        public async Task<IActionResult> PostProductModel(int id, ProductModel ProductModel)
        {
            ProductModel.Order = await _context.Orders.FindAsync(ProductModel.ProductId);

            _context.Products.Add(ProductModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductModel", new {id = ProductModel.ProductId}, ProductModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
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

        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
