using API_Project_cw2.Data;
using API_Project_cw2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_cw2.Controllers
{
    public class ProductsController : Controller
    {
        
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _context.Product.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return Created("created", product);
        }
    
    
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(
            [FromQuery] string? name,
            [FromQuery] int? price,
            [FromQuery] string? description,
            [FromQuery] int? count)
        
        {
            var products = _context.Product.AsQueryable();
            if (name != null)
            {
                products = products.Where(x => x.Name == name);
            }
            if (price != null)
            {
                products = products.Where(x => x.Price == price);
            }
            if (description != null)
            {
                products = products.Where(x => x.Description == description);
            }
            
            if (count != null)
            {
                products = products.Where(x => x.Count == count);
            }
        
            return Ok(await products.ToListAsync());
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveProduct([FromRoute] int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
        {
            product.Id = id;
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    
        [HttpPatch ("{id}")]
        public async Task<ActionResult<Product>> UpdatePartialProduct(int id, [FromBody] int price)
        {
            var product = await _context.Product.FindAsync(id);
            product.Price = price;
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        
        //-----------------------------------------------------------------
        
        [HttpDelete]
        public async Task<ActionResult> RemoveProducts([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                return BadRequest("No product IDs provided.");
            
            var products = await _context.Product.Where(p => ids.Contains(p.Id)).ToListAsync();

            if (products.Count == 0)
                return NotFound("No products found with the provided IDs.");
            
            _context.Product.RemoveRange(products);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        
        [HttpGet ("sorted")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string sortOrder = "asc")
        {
            IQueryable<Product> products = _context.Product;

            switch (sortOrder.ToLower())
            {
                case "desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "asc":
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return Ok(await products.ToListAsync());
        }
        

    }
}
