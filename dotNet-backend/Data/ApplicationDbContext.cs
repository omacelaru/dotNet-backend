
using dotNet_backend.Models.Club;
using dotNet_backend.Models.Coach;
using dotNet_backend.Models.User;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Coach> Coaches { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Club>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Club>()
            .HasMany(c => c.Coaches)
            .WithOne(c => c.Club)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Coach>()
            .HasOne(c => c.User)
            .WithOne(c => c.Coach)
            .HasForeignKey<Coach>(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
