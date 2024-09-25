namespace SimpleBlog.IntegrationTests;

[Collection("Database")]
public class BaseE2ETest : BaseDataTest, IClassFixture<CustomWebApplicationFactory>
{
    protected readonly HttpClient Client;
    
    protected BaseE2ETest(DatabaseFixture databaseFixture, CustomWebApplicationFactory factory) : base(databaseFixture)
    {
        Client = factory.CreateClient();
    }
}