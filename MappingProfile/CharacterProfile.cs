using AutoMapper;

namespace dotnetRPG.MappingProfile;

public class CharacterProfile: Profile
{

    public CharacterProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
    }
}