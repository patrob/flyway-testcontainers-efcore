namespace SimpleBlog.Api.Repositories;

public static class ServiceRegister
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        return services.AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IPostRepository, PostRepository>()
            .AddTransient<IPostDetailRepository, PostDetailRepository>()
            .AddTransient<IUserStatsRepository, UserStatsRepository>();
    }
}