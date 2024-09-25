using Microsoft.EntityFrameworkCore;

namespace SimpleBlog.Api.Data;

public static class ServiceRegister
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<BlogDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("BlogDb"))
                .UseSnakeCaseNamingConvention()
                );
    }
}