
namespace dotnetRPG.Data;

public class DataContext : DbContext
{
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<Skill> Skills => Set<Skill>();

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>().HasData(
            new Skill { Id = 1, Name = "Fireball", Damage = 30 },
            new Skill { Id = 2, Name = "Blizzard", Damage = 40 },
            new Skill { Id = 3, Name = "Ashkal", Damage = 20 },
            new Skill { Id = 4, Name = "Frenzy", Damage = 10 }
            );
    }

}