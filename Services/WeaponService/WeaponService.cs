using AutoMapper;
using dotnetRPG.Data;
using dotnetRPG.Dtos.Character;
using dotnetRPG.Dtos.Weapon;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace dotnetRPG.Services.WeaponService;

public class WeaponService : IWeaponService
{

    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public WeaponService(DataContext dataContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }


    public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
    {
        var response = new ServiceResponse<GetCharacterDto>();

        try
        {

            var character = await _dataContext.Characters
                .FirstOrDefaultAsync(

                c => c.Id == newWeapon.CharacterId &&
                c.User!.Id == int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!)
                );


            if (character is null)
            {
                response.Success = false;
                response.Message = "Character not found";
                return response;
            }

            var weaponToCreate = _mapper.Map<Weapon>(newWeapon);
            //   weaponToCreate.CharacterId = int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            await _dataContext.Weapons.AddAsync(weaponToCreate);
            await _dataContext.SaveChangesAsync();

            response.Data = _mapper.Map<GetCharacterDto>(character);
            response.Success = true;
            response.Message = $"Weapon {newWeapon.Name} saved!";
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }

    }

    public async Task<ServiceResponse<GetWeaponDto>> DeleteWeapon(int delCharacterId)
    {

        var response = new ServiceResponse<GetWeaponDto>();

        try
        {
            var validCharacter = await _dataContext.Characters
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(
                c => c.User!.Id == int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!)
                && c.Id == delCharacterId);

            if (validCharacter is null)
            {
                response.Success = false;
                response.Message = "Invalid character combinason";
                return response;
            }



            var currentCharacterWeapon = await _dataContext.Weapons.FirstOrDefaultAsync(w => w.CharacterId == validCharacter.Weapon!.CharacterId);

            if (currentCharacterWeapon is null)
            {
                response.Message = "Current character has no weapon yet!";
                response.Success = false;
                return response;
            }


            _dataContext.Weapons.Remove(currentCharacterWeapon!);
            await _dataContext.SaveChangesAsync();
            response.Message = $"Weapon deleted successfuly from character";
            response.Data = _mapper.Map<GetWeaponDto>(currentCharacterWeapon);

            return response;

        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Success = false;
        }

        return response;
    }
}
