using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleBlog.Api.Data;

namespace SimpleBlog.IntegrationTests;

[Collection("Database")]
public class CustomWebApplicationFactory(DatabaseFixture databaseFixture) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Local");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<BlogDbContext>()
                .AddDbContext<BlogDbContext>(options =>
                    options.UseNpgsql(databaseFixture.GetConnectionString())
                        .UseSnakeCaseNamingConvention());
        });
        base.ConfigureWebHost(builder);
    }
}