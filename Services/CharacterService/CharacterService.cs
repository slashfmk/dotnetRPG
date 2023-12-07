using dotnetRPG.Dtos;

namespace dotnetRPG.Services.CharacterService;

public class CharacterService : ICharacterService
{
   
    private static List<Character> _characters = new List<Character>
    {
        new Character
        {
            Id = 2, Name = "motaro", Class = RpgClass.Knight, Defense = 12, Intelligence = 5, Strength = 10,
            HitPoints = 40
        },
        new Character
        {
            Id = 3, Class = RpgClass.Cleric, Defense = 9, Intelligence = 12, Name = "Jaden", Strength = 22,
            HitPoints = 43
        }
    };


    public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
    {
        var response = new ServiceResponse<List<Character>>();

        response.Data = _characters.ToList();
        response.Message = $" {_characters.Count()} users found";
        response.Success = true;
        
        return  response;
    }

    // Get character by Id
    public async Task<ServiceResponse<Character>> GetCharacterById(int id)
    {
        var foundCharacter = _characters.FirstOrDefault(x => x.Id == id);
        var response = new ServiceResponse<Character>();
        
        if (foundCharacter is null)
        {
            response.Data = null;
            response.Message = $"Character not found!";
            response.Success = false;
        }


        response.Data = foundCharacter;

        return response;
    }

    // Delete character
    public async Task<ServiceResponse<Character>> DeleteCharacter(int id)
    {
        var found = _characters.FirstOrDefault(c => c.Id == id);

        var response = new ServiceResponse<Character>();

        if (found is null)
        {
            response.Data = null;
            response.Message = "Character not existing";
            response.Success = false;
        }

        _characters.Remove(found);
        
        response.Data = found;
        
        return response;
    }

    // Add new character
    public async Task<ServiceResponse<Character>> AddCharacter(CharacterDto newCharacter)
    {
        var idCount = _characters.Count + 1;

        var response = new ServiceResponse<Character>();
        
        var newToInsert = new Character
        {
            Id = idCount,
            Class = newCharacter.Class,
            Defense = newCharacter.Defense,
            Name = newCharacter.Name,
            Intelligence = newCharacter.Intelligence,
            HitPoints = newCharacter.HitPoints,
            Strength = newCharacter.Strength
        };

        _characters.Add(newToInsert);

        response.Data = newToInsert;
        response.Message = "Character successfully added!";

        return response;
    }
}