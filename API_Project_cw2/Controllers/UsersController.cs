using API_Project_cw2.Data;
using API_Project_cw2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Project_cw2.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }
    
    // отримання всіх користувачів
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return Ok(await _context.User.ToListAsync()); // Ok() - status 200
    }

    // створення нового користувача
    [HttpPost]
    public async Task<ActionResult<User>> AddUser(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
        return Created("created", user);
    }

    // пошук користувачів
    [HttpGet("searchUser")]
    public async Task<ActionResult<IEnumerable<User>>> SearchUsers(
        [FromQuery] string? name,
        [FromQuery] string? lastName,
        [FromQuery] int? age)
    {
        var users = _context.User.AsQueryable();
        if (name != null)
        {
            users = users.Where(x => x.Name == name);
        }
    
        if (lastName != null)
        {
            users = users.Where(x => x.LastName == lastName);
        }
    
        if (age != null)
        {
            users = users.Where(x => x.Age == age);
        }
    
        return Ok(await users.ToListAsync()); // status 200
    }

    //DELETE api/users/1
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        var user = await _context.User.FindAsync(id);
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // оновлення користувача
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
    {
        user.Id = id;
        _context.User.Update(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }

    // часткове оновлення користувача
    [HttpPatch("{id}")]
    public async Task<ActionResult<User>> UpdatePartialUser(int id, [FromBody] string name)
    {
        var user = await _context.User.FindAsync(id);
        user.Name = name;
        await _context.SaveChangesAsync();
        return Ok(user);
    }
}
