using System.ComponentModel.DataAnnotations;

namespace API_Project_cw2.Requests;

public class CreateUserRequest
{
    public string Nickname { get; set; }
    
    public string Password { get; set; }
}