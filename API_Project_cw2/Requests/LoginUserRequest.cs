using System.ComponentModel.DataAnnotations;
using API_Project_cw2.Models;

namespace API_Project_cw2.Requests;

public class LoginUserRequest
{
    public string Nickname { get; set; }
    public string Password { get; set; }
}