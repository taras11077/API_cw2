using API_Project_cw2.Models;

namespace API_Project_cw2.DTOs;

public class UserDTO
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public DateTime LastSeenOnline { get; set; }
}