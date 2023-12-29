using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetRPG.Models;

public class Skill
{
    public int Id { get; set; }
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }
    public List<Character>? Characters { get; set; }
}
