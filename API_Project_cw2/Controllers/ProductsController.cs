using API_Project_cw2.Data;
using API_Project_cw2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_cw2.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : Controller
{
    private readonly DataContext _context;
    public ProductsController(DataContext context)
    {
        _context = context;
    }
    
    // отримання всіх продуктів
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        try
        {
            return Ok(await _context.Product.ToListAsync());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    // створення продукту
    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product, [FromQuery] int? userId)
    {
        try
        {
            if (userId.HasValue)
            {
                var user = await _context.User.FindAsync(userId);

                if (user == null)
                    return NotFound("User not found.");

                product.User = user;
            }    
            
            
            _context.Add(product);
            await _context.SaveChangesAsync();

            return Created("created", product);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
    
    // видалення продукту
    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveProduct([FromRoute] int id)
    {
        try
        {
            var product = await _context.Product.FindAsync(id);
        
            if (product == null)
                return NotFound($"Product with ID {id} not found.");
        
        
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    // оновлення продукту
    [HttpPut ("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] Product product)
    {
        try
        {
            product.Id = id;
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }

    // часткове оновлення продукту
    [HttpPatch ("{id}")]
    public async Task<ActionResult<Product>> UpdatePartialProduct(int id, [FromBody] int price)
    {
        try
        {
            var product = await _context.Product.FindAsync(id);
        
            if (product == null)
                return NotFound($"Product with ID {id} not found.");
        
            product.Price = price;
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
    
    // видалення декількох продуктів
    [HttpDelete]
    public async Task<ActionResult> RemoveProducts([FromBody] List<int> ids)
    {
        try
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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
    
    // сортування продуктів
    [HttpGet("sorted")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string sortOrder = "asc")
    {
        try
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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
    }
    
    // пошук продуктів за ключовим словом в описі
    [HttpGet("search-by-keyword")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByDescription([FromQuery] string keyword)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Keyword cannot be empty");
        
            var products = await _context.Product
                .Where(p => p.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

            if (!products.Any())
                return NotFound("No products found matching the description.");
        
            return Ok(products);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
       
    }
    
    // пошук продуктів за користувачем
    [HttpGet("search-by-user")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByUser([FromQuery] int? userId)
    {
        try
        {
            if (userId == null)
                return BadRequest("User ID cannot be empty");
        
            var products = await _context.Product
                .Where(p => p.User.Id == userId.Value)
                .ToListAsync();

            if (!products.Any())
                return NotFound("No products found that match the condition.");
        
            return Ok(products);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
        }
        
    }
    
}

