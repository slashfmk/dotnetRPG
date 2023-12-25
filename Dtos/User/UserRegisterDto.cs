using System.ComponentModel.DataAnnotations;

namespace dotnetRPG.Dtos.User;

public class UserRegisterDto
{
    [Required]
    public string Username { get; set; } = string.Empty;
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}