using dotnetRPG.Dtos.Character;
using dotnetRPG.Dtos.Weapon;
using dotnetRPG.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRPG.Controllers
{

    [Authorize]
    public class WeaponController : BaseApiController
    {

        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService; ;
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<GetWeaponDto>>> DeleteWeapon()
        {
            var response = await _weaponService.DeleteWeapon();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetWeaponDto>>> AddWeapon(AddWeaponDto weapon)
        {

            var result = await _weaponService.AddWeapon(weapon);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }



    }
}
