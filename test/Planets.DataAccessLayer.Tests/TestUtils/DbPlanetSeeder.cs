using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Planets.DataAccessLayer.Models;

namespace Planets.DataAccessLayer.IntegrationTests.TestUtils;

public static class DbPlanetSeeder
{
    public static async Task SeedPlanets(IAmazonDynamoDB db, string tableName, IEnumerable<Planet> planets)
    {
        var table = Table.LoadTable(db, tableName);

        foreach (var planet in planets)
        {
            await table.PutItemAsync(new Document
            {
                ["pk"] = "planet/list",
                ["sk"] = planet.ID,
                ["ID"] = planet.ID,
                ["Name"] = planet.Name,
                ["ImageUrl"] = planet.ImageUrl,
                ["DistanceToSun"] = new Document
                {
                    ["Value"] = planet.DistanceToSun.Value,
                    ["Unit"] = planet.DistanceToSun.Unit,
                },
                ["Mass"] = new Document
                {
                    ["Value"] = planet.Mass.Value,
                    ["Unit"] = planet.Mass.Unit,
                },
                ["Diameter"] = new Document
                {
                    ["Value"] = planet.Diameter.Value,
                    ["Unit"] = planet.Diameter.Unit,
                },
                ["AdditionalInfo"] = planet.AdditionalInfo,
            });
        }
    }
}
