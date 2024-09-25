using System.Data.Common;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Npgsql;
using Respawn;

namespace SimpleBlog.IntegrationTests;

public class DatabaseFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer;
    private readonly IContainer _flywayContainer;
    
    public const string FlywayImage = "flyway/flyway:10-alpine";

    private const string DbContainerName = "postgres-db";
    private const string Username = "postgres";
    private const string Password = "postgres";
    private const int Port = 5432;

    private readonly string[] _flywayCommands =
    [
        $"-url=jdbc:postgresql://{DbContainerName}:{Port}/postgres",
        $"-user={Username}",
        $"-password={Password}",
        "-connectRetries=20",
        "-placeholders.env=local",
        "migrate"
    ];

    
    private Respawner _respawner = default!;

    public string GetConnectionString() => _postgreSqlContainer.GetConnectionString();

    public DbConnection GetOpenDbConnection()
    {
        var connection = new NpgsqlConnection(GetConnectionString());
        connection.Open();
        return connection;
    }

    public async Task ResetDatabase() => await _respawner.ResetAsync(GetOpenDbConnection());

    public DatabaseFixture()
    {
        var flywayDirectory = GetFlywayDirectory();

        var network = new NetworkBuilder()
            .Build();

        _postgreSqlContainer = new PostgreSqlBuilder()
            .WithName(DbContainerName)
            .WithUsername(Username)
            .WithPassword(Password)
            .WithPortBinding(Port)
            .WithNetwork(network)
            .WithAutoRemove(true)
            .Build();

        _flywayContainer = new ContainerBuilder()
            .WithImage(FlywayImage)
            .WithName("flyway")
            .WithResourceMapping(flywayDirectory, "/flyway/sql")
            .WithEntrypoint("flyway")
            .WithCommand(_flywayCommands)
            .WithNetwork(network)
            .WithAutoRemove(true)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();
        await _flywayContainer.StartAsync();
        await _flywayContainer.GetExitCodeAsync();

        _respawner = await Respawner.CreateAsync(GetOpenDbConnection(), new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"]
        });
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
    
    private string GetRootDirectory()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var rootDirectory = currentDirectory[..currentDirectory.IndexOf("/SimpleBlog.IntegrationTests", StringComparison.Ordinal)];
        return rootDirectory;
    }

    private string GetFlywayDirectory()
    {
        var rootDirectory = GetRootDirectory();
        var hostFlywayDirectory = Path.Combine(rootDirectory, "flyway/sql/");
        return hostFlywayDirectory;
    }
}

[CollectionDefinition("Database")]
public class DatabaseFixtureCollection : ICollectionFixture<DatabaseFixture>;