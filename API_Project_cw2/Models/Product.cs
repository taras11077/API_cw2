namespace API_Project_cw2.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Count { get; set; }
    
    public virtual User? User { get; set; }
}