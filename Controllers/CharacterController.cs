global using dotnetRPG.Models;
using dotnetRPG.Dtos;
using Microsoft.AspNetCore.Mvc;


namespace dotnetRPG.Controllers
{
    public class CharacterController : BaseApiController
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var result = await _characterService.DeleteCharacter(id);

            if (!result.Success) return NotFound((result));
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Create(AddCharacterDto newAddCharacter)
        {
            return Ok(await _characterService.AddCharacter(newAddCharacter));
        }
    }
}