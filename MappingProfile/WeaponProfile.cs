using AutoMapper;
using dotnetRPG.Dtos.Weapon;

namespace dotnetRPG.MappingProfile;

public class WeaponProfile : Profile
{
    public WeaponProfile()
    {
        CreateMap<AddWeaponDto, Weapon>();
        CreateMap<Weapon, GetWeaponDto>();
    }
}
