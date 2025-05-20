using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using skillJas.Infrastructure.Data;

namespace skillJas.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<skillJasDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
}
