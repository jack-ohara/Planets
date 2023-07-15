using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace Planets.DataAccessLayer.Tests;


public class DynamoDBTestFixture : IAsyncLifetime
{

    public async Task InitializeAsync()
    {
        var config = new AmazonDynamoDBConfig();
        config.RegionEndpoint = RegionEndpoint.EUWest1;
        config.ServiceURL = "http://localhost:8000";
        Db = new AmazonDynamoDBClient(new BasicAWSCredentials("myFakeAccessKey", "myFakeSecretKey"), config);

        TableName = Guid.NewGuid().ToString();

        await Db.CreateTableAsync(
            TableName,
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
        await Db.DeleteTableAsync(TableName);
        Db.Dispose();
    }

    public IAmazonDynamoDB Db { get; private set; }
    public string TableName { get; private set; }
}


