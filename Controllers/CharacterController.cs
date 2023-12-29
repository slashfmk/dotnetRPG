global using dotnetRPG.Models;
using dotnetRPG.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace dotnetRPG.Controllers;

[Authorize]
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

    [HttpPost("AddSkill")]
    public async Task<ActionResult<GetCharacterDto>> AddSkill(AddCharacterSkillDto addCharacterSkillDto)
    {
        var result = await _characterService.AddCharacterSkill(addCharacterSkillDto);

        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}