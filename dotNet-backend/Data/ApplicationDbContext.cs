
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Athlete> Athletes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().UseTpcMappingStrategy();

        modelBuilder.Entity<Club>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Club>()
            .HasMany(c => c.Coaches)
            .WithOne(c => c.Club)
            .HasForeignKey(c => c.ClubId);

        modelBuilder.Entity<Coach>()
            .HasMany(c => c.Athletes)
            .WithOne(a => a.Coach)
            .HasForeignKey(a => a.CoachId);

        base.OnModelCreating(modelBuilder);
    }
}
