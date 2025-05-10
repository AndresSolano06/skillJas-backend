using Microsoft.EntityFrameworkCore;
using skillJas.Domain.Entities;

namespace skillJas.Infrastructure.Data;

public class SkillJasDbContext : DbContext
{
    public SkillJasDbContext(DbContextOptions<SkillJasDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Favorite> Favorites => Set<Favorite>();
    public DbSet<Progress> Progress => Set<Progress>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Course>().HasKey(c => c.Id);
        modelBuilder.Entity<Favorite>().HasKey(f => f.Id);
        modelBuilder.Entity<Progress>().HasKey(p => p.Id);

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favorites)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<Favorite>()
            .HasOne(f => f.Course)
            .WithMany(c => c.Favorites)
            .HasForeignKey(f => f.CourseId);

        modelBuilder.Entity<Progress>()
            .HasOne(p => p.User)
            .WithMany(u => u.Progress)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Progress>()
            .HasOne(p => p.Course)
            .WithMany(c => c.Progress)
            .HasForeignKey(p => p.CourseId);
    }
}
