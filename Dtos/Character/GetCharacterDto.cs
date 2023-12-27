using dotnetRPG.Dtos.Skills;
using dotnetRPG.Dtos.Weapon;

namespace dotnetRPG.Dtos.Character;

public class GetCharacterDto
{

    public int Id { get; set; }
    public string? Name { get; set; }
    public int HitPoints { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    public int Intelligence { get; set; }
    public RpgClass Class { get; set; }
    public GetWeaponDto? Weapon { get; set; }
    public GetSkillDto? Skill { get; set; }

}