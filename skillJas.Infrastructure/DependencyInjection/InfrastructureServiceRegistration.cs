using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SkillJas.Infrastructure.Data;

namespace SkillJas.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<SkillJasDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
