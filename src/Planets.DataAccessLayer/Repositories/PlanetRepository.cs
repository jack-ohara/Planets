using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace Planets.DataAccessLayer.Repositories;

public class PlanetRepository
{
    private Table _table;

    public PlanetRepository(IAmazonDynamoDB client, string tableName)
    {
        _table = Table.LoadTable(client, tableName);
    }

    public object GetAllPlanets()
    {
        var query = new QueryFilter("pk", QueryOperator.Equal, "planet/list");

        var result = _table.Query(query);

        return new[] { new { } };
    }
}
