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
        public async Task<ActionResult<List<Character>>> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> GetById(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Character>> DeleteCharacter(int id)
        {
            return Ok(await _characterService.DeleteCharacter(id));
        }

        [HttpPost]
        public async Task<ActionResult<Character>> Create(CharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
    }
}