using dotnetRPG.Dtos;

namespace dotnetRPG.Services.CharacterService;

public interface ICharacterService
{
    Task<ServiceResponse<List<Character>>> GetAllCharacters();
    Task<ServiceResponse<Character>> GetCharacterById(int id);
    Task<ServiceResponse<Character>> DeleteCharacter(int id);
    Task<ServiceResponse<Character>> AddCharacter(CharacterDto newCharacter);
}