using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using skillJas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace skillJas.Infrastructure.Data
{
    public class skillJasDbContext : DbContext, ISkillJasDbContext
    {
        public skillJasDbContext(DbContextOptions<skillJasDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Favorite> Favorites => Set<Favorite>();
        public DbSet<Documentation> Documentation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Course>().HasKey(c => c.Id);
            modelBuilder.Entity<Favorite>().HasKey(f => f.Id);
            modelBuilder.Entity<Documentation>().HasKey(d => d.Id);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Course)
                .WithMany(c => c.Favorites)
                .HasForeignKey(f => f.CourseId);

            modelBuilder.Entity<Course>()
                .Property(c => c.Category)
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .Metadata.SetValueComparer(
                    new ValueComparer<List<string>>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToList())
                );

            modelBuilder.Entity<Documentation>()
                .Property(d => d.Title)
                .IsRequired();

            modelBuilder.Entity<Documentation>()
                .Property(d => d.Description)
                .IsRequired();

            modelBuilder.Entity<Documentation>()
                .Property(d => d.Url)
                .IsRequired();

        }
    }
}
