using API_Project_cw2.Interfaces;
using API_Project_cw2.Models;

namespace API_Project_cw2.Services;

public class ProductService : IProductService
{
    private readonly IRepository _repository;

    public ProductService(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Product> AddProduct(Product product)
    {
        return await _repository.Add(product);
    }

    public IEnumerable<Product> GetProducts(int page, int size)
    {
        return _repository.GetAll<Product>()
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
    }

    public IEnumerable<Product> SearchProducts(string name)
    {
        return _repository.GetAll<Product>()
            .Where(u => u.Name.ToLower().Contains(name.ToLower()))
            .ToList();
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _repository.GetById<Product>(id);
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        return await _repository.Update(product);
    }

    public async Task DeleteProduct(int id)
    {
        await _repository.Delete<Product>(id);
    }
}