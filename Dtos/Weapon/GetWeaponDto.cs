
using dotnetRPG.Dtos.Character;
namespace dotnetRPG.Dtos.Weapon;

public class GetWeaponDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }
   // public Character? Character { get; set; }

    // Foreign key for one to one weapon_character relationship
    public int CharacterId { get; set; }
}
