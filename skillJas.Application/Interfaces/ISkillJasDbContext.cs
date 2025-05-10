using Microsoft.EntityFrameworkCore;
using skillJas.Domain.Entities;
using System.Collections.Generic;

public interface ISkillJasDbContext
{
    DbSet<User> Users { get; }
    DbSet<Course> Courses { get; }
    DbSet<Favorite> Favorites { get; }
    DbSet<Progress> Progress { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
