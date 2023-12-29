using dotnetRPG.Dtos.Character;

namespace dotnetRPG.Services.CharacterService;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(int id);
    Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newAddCharacter);

    Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkillDto);
}