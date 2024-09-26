using System.Data.Common;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using SimpleBlog.Api.Data;

namespace SimpleBlog.IntegrationTests;

public class DatabaseFixture : IAsyncLifetime
{
    private const string DbContainerName = "postgres-db";
    private const string Username = "postgres";
    private const string Password = "postgres";
    private const int Port = 5432;
    private readonly PostgreSqlContainer _postgreSqlContainer= new PostgreSqlBuilder()
        .WithName(DbContainerName)
        .WithUsername(Username)
        .WithPassword(Password)
        .WithPortBinding(Port)
        .WithAutoRemove(true)
        .Build();
    
    private Respawner _respawner = default!;

    public string GetConnectionString() => _postgreSqlContainer.GetConnectionString();

    public DbConnection GetOpenDbConnection()
    {
        var connection = new NpgsqlConnection(GetConnectionString());
        connection.Open();
        return connection;
    }

    public async Task ResetDatabase() => await _respawner.ResetAsync(GetOpenDbConnection());

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        await MigrateDatabase();

        _respawner = await Respawner.CreateAsync(GetOpenDbConnection(), new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"]
        });
    }

    private async Task MigrateDatabase()
    {
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseNpgsql(GetOpenDbConnection())
            .UseSnakeCaseNamingConvention()
            .Options;
        var context = new BlogDbContext(options);
        await context.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}

[CollectionDefinition("Database")]
public class DatabaseFixtureCollection : ICollectionFixture<DatabaseFixture>;