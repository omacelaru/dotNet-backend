
using dotNet_backend.Models.Athlete;
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.Competition;
using dotNet_backend.Models.Participation;
using dotNet_backend.Models.Request;
using dotNet_backend.Models.User;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS1591
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Coach> Coaches { get; set; }
    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<RequestInfo> Requests { get; set; }
    public DbSet<Participation> Participations { get; set; }
    public DbSet<Competition> Competitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().UseTptMappingStrategy();

        modelBuilder.Entity<Club>().HasIndex(c => c.Name).IsUnique();

        modelBuilder.Entity<Coach>()
            .HasOne(c => c.Club)
            .WithOne(c => c.Coach)
            .HasForeignKey<Club>(c => c.CoachId)
            .IsRequired();
        modelBuilder.Entity<Coach>()
            .HasMany(c => c.Athletes)
            .WithOne(a => a.Coach)
            .HasForeignKey(a => a.CoachId);
        
        modelBuilder.Entity<Participation>()
            .HasKey(p => new {p.AthleteId, p.CompetitionId});
        modelBuilder.Entity<Participation>()
            .HasOne(p => p.Athlete)
            .WithMany(a => a.Participations)
            .HasForeignKey(p => p.AthleteId);
        modelBuilder.Entity<Participation>()
            .HasOne(p => p.Competition)
            .WithMany(c => c.Participations)
            .HasForeignKey(p => p.CompetitionId);

        base.OnModelCreating(modelBuilder);
    }
}
#pragma warning restore CS1591