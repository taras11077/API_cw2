using API_Project_cw2.Models;

namespace API_Project_cw2.Data;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(
    //         "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProductShopDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    // }
    
    public DbSet<Product> Product { get; set; }
    public DbSet<User> User { get; set; }
}