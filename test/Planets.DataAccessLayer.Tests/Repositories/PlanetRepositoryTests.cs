using Planets.DataAccessLayer.Repositories;
using FluentAssertions;
using Planets.DataAccessLayer.Tests.TestUtils;

namespace Planets.DataAccessLayer.Tests;

public class PlanetRepositoryTests : IClassFixture<DynamoDBTestFixture>
{
    DynamoDBTestFixture fixture;

    public PlanetRepositoryTests(DynamoDBTestFixture fixture)
    {
        this.fixture = fixture;
    }


    [Fact]
    public async Task GetAllPlanets_ShouldReturnAllPlanetsAsync()
    {
        var planets = new[]
        {
            new PlanetBuilder().Build(),
            new PlanetBuilder().WithAdditionalInfo("This planet is really cool!").Build()
        };

        await DbPlanetSeeder.SeedPlanets(fixture.Db, fixture.TableName, planets);

        var repo = new PlanetRepository(fixture.Db, fixture.TableName);

        var result = await repo.GetAllPlanets();

        result.Should().BeEquivalentTo(planets);
    }

    [Fact]
    public async Task GetPlanet_ShouldReturnAllInfoForPlanetAsync()
    {
        var planet = new PlanetBuilder().Build();

        await DbPlanetSeeder.SeedPlanets(fixture.Db, fixture.TableName, new[] { planet });

        var repo = new PlanetRepository(fixture.Db, fixture.TableName);

        var result = await repo.GetPlanet(planet.ID);

        result.Should().BeSameAs(planet);
    }
}

