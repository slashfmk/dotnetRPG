using AutoMapper;
using dotnetRPG.Dtos.Character;

namespace dotnetRPG.MappingProfile;

public class CharacterProfile: Profile
{

    public CharacterProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
    }
}