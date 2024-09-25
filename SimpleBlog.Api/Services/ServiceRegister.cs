namespace SimpleBlog.Api.Services;

public static class ServiceRegister
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        return services.AddTransient<IUserService, UserService>()
            .AddTransient<IPostService, PostService>()
            .AddTransient<IPostDetailService, PostDetailService>()
            .AddTransient<IUserStatsService, UserStatsService>();
    }
}