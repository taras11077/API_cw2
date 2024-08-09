using API_Project_cw2.Models;

namespace API_Project_cw2.Data;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Product { get; set; }
    public DbSet<User> User { get; set; }
}