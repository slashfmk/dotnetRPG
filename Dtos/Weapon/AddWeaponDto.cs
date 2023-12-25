using System.ComponentModel.DataAnnotations;

namespace dotnetRPG.Dtos.Weapon;

public class AddWeaponDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(1, 500)]
    public int Damage { get; set; }
}
