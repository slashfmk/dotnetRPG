using System.ComponentModel.DataAnnotations;

namespace dotnetRPG.Dtos;

public class CharacterDto
{
    // public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int HitPoints { get; set; }
    [Required]
    public int Strength { get; set; }
    [Required]
    public int Defense { get; set; }
    [Required]
    public int Intelligence { get; set; }
    public RpgClass Class { get; set; } = RpgClass.Knight;
}