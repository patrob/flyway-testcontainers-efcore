
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Api.Data;
using SimpleBlog.Api.Mapping;

namespace SimpleBlog.IntegrationTests;

[Collection("Database")]
public class BaseDataTest
{
    protected readonly BlogDbContext Context;
    private readonly Fixture _fixture;
    private readonly Random _randy = new();
    protected readonly IMapper Mapper;

    protected BaseDataTest(DatabaseFixture databaseFixture)
    {
        var options = new DbContextOptionsBuilder<BlogDbContext>()
            .UseNpgsql(databaseFixture.GetConnectionString())
            .UseSnakeCaseNamingConvention()
            .Options;
        Context = new BlogDbContext(options);
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new NullRecursionBehavior());
        var mapperConfig = new MapperConfiguration(cfg => cfg.AddMaps(typeof(Program)));
        Mapper = new Mapper(mapperConfig);
        databaseFixture.ResetDatabase().Wait();
    }
    
    protected T GetFakeModel<T>(Func<T, T>? f = null)
    {
        var model = _fixture.Create<T>();
        if (f != null)
        {
            model = f(model);
        }

        return model;
    }

    protected List<T> GetFakeModels<T>(Func<T, T>? f = null, int? count = null)
    {
        var c = count ?? _randy.Next(3, 10);
        var models = Enumerable
            .Range(1, c)
            .Select(c => GetFakeModel(f))
            .ToList();

        return models;
    }
}