using API_Project_cw2.Models;

namespace API_Project_cw2.Interfaces;

public interface IUserService
{
    Task<User> Login(string nickname, string password);
    Task<User> Register(string nickname, string password);
    Task<User> GetUserById(int id);
    Task<User> UpdateUser(User user);
    
    Task DeleteUser(int id);
    
    IEnumerable<User> GetUsers(int page, int size);
    IEnumerable<User> SearchUsers(string nickname);
}