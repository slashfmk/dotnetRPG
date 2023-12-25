using AutoMapper;
using dotnetRPG.Data;
using dotnetRPG.Dtos.Character;
using System.Security.Claims;


namespace dotnetRPG.Services.CharacterService;

public class CharacterService : ICharacterService
{
    // Members
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public CharacterService(IMapper mapper, DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        _mapper = mapper;
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
    }

    // Get the user Id from claims
    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);



    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var response = new ServiceResponse<List<GetCharacterDto>>();
        var users = await _dataContext.Characters.Where(c => c.User!.Id == GetUserId()).ToListAsync();

        response.Data = _mapper.Map<List<GetCharacterDto>>(users);
        response.Message = $" {users.Count()} users found";
        response.Success = true;

        return response;
    }

    // Get character by Id
    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        
        var foundCharacter = await _dataContext.Characters.FirstOrDefaultAsync(x => x.Id == id && x.User!.Id == this.GetUserId());
        // var foundCharacter = await _dataContext.Characters.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id && x.User!.Id == this.GetUserId());
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
        var found = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == this.GetUserId());

        var response = new ServiceResponse<GetCharacterDto>();

        if (found is null)
        {
            response.Data = null;
            response.Message = "Character not existing";
            response.Success = false;
            return response;
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
            characterToSave.User = await _dataContext.Users.FirstOrDefaultAsync(c => c.Id == this.GetUserId());

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