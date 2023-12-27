using dotnetRPG.Dtos.Character;
using dotnetRPG.Dtos.Weapon;

namespace dotnetRPG.Services.WeaponService;
public interface IWeaponService
{

    public Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    public Task<ServiceResponse<GetWeaponDto>> DeleteWeapon(int delCharacterId);

}
