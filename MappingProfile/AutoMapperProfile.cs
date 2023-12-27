using AutoMapper;
using dotnetRPG.Dtos.Character;
using dotnetRPG.Dtos.Skills;
using dotnetRPG.Dtos.Weapon;

namespace dotnetRPG.MappingProfile;

public class AutoMapperProfile: Profile
{

    public AutoMapperProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();

        CreateMap<AddWeaponDto, Weapon>();
        CreateMap<Weapon, GetWeaponDto>();

        CreateMap<Skill, GetSkillDto>(); 

    }
}