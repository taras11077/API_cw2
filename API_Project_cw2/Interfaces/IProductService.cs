using API_Project_cw2.Models;

namespace API_Project_cw2.Interfaces;

public interface IProductService
{
    Task<Product> AddProduct(Product product);
    
    IEnumerable<Product> GetProducts(int page, int size);
    IEnumerable<Product> SearchProducts(string name);
    Task<Product> GetProductById(int id);
    
    Task<Product> UpdateProduct(Product product);
    
    Task DeleteProduct(int id);
}