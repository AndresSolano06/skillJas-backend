using Microsoft.EntityFrameworkCore;
using skillJas.Domain.Entities;
using System.Collections.Generic;

public interface ISkillJasDbContext
{
    DbSet<Course> Courses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
