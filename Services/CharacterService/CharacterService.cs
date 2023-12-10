using AutoMapper;
using dotnetRPG.Data;
using dotnetRPG.Dtos;


namespace dotnetRPG.Services.CharacterService;

public class CharacterService : ICharacterService
{

    // Members
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;


    public CharacterService(IMapper mapper, DataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var response = new ServiceResponse<List<GetCharacterDto>>();
        var users = await _dataContext.Characters.ToListAsync();
        
        response.Data = _mapper.Map<List<GetCharacterDto>>(users);
        response.Message = $" {users.Count()} users found";
        response.Success = true;

        return response;
    }

    // Get character by Id
    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var foundCharacter = await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == id);
        var response = new ServiceResponse<GetCharacterDto>();

        if (foundCharacter is null)
        {
            response.Data = null;
            response.Message = $"Character not found!";
            response.Success = false;
        }


        response.Data = _mapper.Map<GetCharacterDto>(foundCharacter);

        return response;
    }

    // Delete character
    public async Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(int id)
    {
        var found = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id);

        var response = new ServiceResponse<GetCharacterDto>();

        if (found is null)
        {
            response.Data = null;
            response.Message = "Character not existing";
            response.Success = false;
        }

        _dataContext.Characters.Remove(found);

        _dataContext.SaveChanges();

        response.Data = _mapper.Map<GetCharacterDto>(found);

        return response;
    }

    // Add new character
    public async Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newAddCharacter)
    {

        var response = new ServiceResponse<GetCharacterDto>();

        try
        {
            var characterToSave = _mapper.Map<Character>(newAddCharacter);
            // characterToSave.Id = _characters.Count + 1;

            await _dataContext.Characters.AddAsync(characterToSave);
            _dataContext.SaveChanges();

            response.Data = _mapper.Map<GetCharacterDto>(characterToSave);
            response.Message = "Character successfully added!";
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        
        return response;
    }
}