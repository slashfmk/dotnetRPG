using System.ComponentModel.DataAnnotations;

namespace dotnetRPG.Dtos;

public class AddCharacterDto
{
    [Required] public string Name { get; set; }
    [Required] public int HitPoints { get; set; }
    [Required] public int Strength { get; set; }
    [Required] public int Defense { get; set; }
    [Required] public int Intelligence { get; set; }
    [Required] public RpgClass Class { get; set; }
}