
using dotNet_backend.Models.Athlete;
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
        modelBuilder.Entity<User>()
            .HasOne(u => u.Coach)
            .WithOne(c => c.User)
            .HasForeignKey<Coach>(c => c.UserId);
        modelBuilder.Entity<User>()
            .HasOne(u => u.Athlete)
            .WithOne(a => a.User)
            .HasForeignKey<Athlete>(a => a.UserId);

        modelBuilder.Entity<Club>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Club>()
            .HasMany(c => c.Coaches)
            .WithOne(c => c.Club);


        base.OnModelCreating(modelBuilder);
    }
}
