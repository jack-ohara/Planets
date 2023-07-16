using Planets.DataAccessLayer.Repositories;
using FluentAssertions;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Planets.DataAccessLayer.IntegrationTests.TestUtils;

namespace Planets.DataAccessLayer.IntegrationTests;

public class PlanetRepositoryTests : IClassFixture<DynamoDBTestFixture>, IAsyncLifetime
{
    DynamoDBTestFixture fixture;
    string _tableName;

    public PlanetRepositoryTests(DynamoDBTestFixture fixture)
    {
        this.fixture = fixture;
        _tableName = Guid.NewGuid().ToString();
    }

    public async Task InitializeAsync()
    {
        await fixture.Db.CreateTableAsync(
            _tableName,
            new List<KeySchemaElement>
            {
                new KeySchemaElement { AttributeName = "pk", KeyType = KeyType.HASH },
                new KeySchemaElement { AttributeName = "sk", KeyType = KeyType.RANGE }
            },
            new List<AttributeDefinition>
            {
                new AttributeDefinition { AttributeName = "pk", AttributeType = ScalarAttributeType.S },
                new AttributeDefinition { AttributeName = "sk", AttributeType = ScalarAttributeType.S },
            },
            new ProvisionedThroughput { ReadCapacityUnits = 1, WriteCapacityUnits = 1 }
        );
    }

    public async Task DisposeAsync()
    {
        await fixture.Db.DeleteTableAsync(_tableName);
    }

    [Fact]
    public async Task GetAllPlanets_ShouldReturnAllPlanets()
    {
        var planets = new[]
        {
            new PlanetBuilder().Build(),
            new PlanetBuilder().WithAdditionalInfo("This planet is really cool!").Build()
        };

        await DbPlanetSeeder.SeedPlanets(fixture.Db, _tableName, planets);

        var repo = new PlanetRepository(fixture.Db, _tableName);

        var result = await repo.GetAllPlanets();

        result.Should().BeEquivalentTo(planets);
    }

    [Fact]
    public async Task GetPlanet_ShouldReturnAllInfoForPlanet()
    {
        var planet = new PlanetBuilder().Build();

        await DbPlanetSeeder.SeedPlanets(fixture.Db, _tableName, new[] { planet });

        var repo = new PlanetRepository(fixture.Db, _tableName);

        var result = await repo.GetPlanet(planet.ID);

        result.Should().BeEquivalentTo(planet);
    }

    [Fact]
    public async Task GetPlanet_WithAnInvalidID_ShouldReturnNull()
    {
        var repo = new PlanetRepository(fixture.Db, _tableName);

        var result = await repo.GetPlanet("does-not-exist");

        result.Should().BeNull();
    }
}

