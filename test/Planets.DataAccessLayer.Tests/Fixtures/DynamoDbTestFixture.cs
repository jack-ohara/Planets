using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;

namespace Planets.DataAccessLayer.Tests;


public class DynamoDBTestFixture : IDisposable
{

    public DynamoDBTestFixture()
    {
        var config = new AmazonDynamoDBConfig();
        config.RegionEndpoint = RegionEndpoint.EUWest1;
        config.ServiceURL = "http://localhost:8000";
        Db = new AmazonDynamoDBClient(new BasicAWSCredentials("myFakeAccessKey", "myFakeSecretKey"), config);
    }

    public void Dispose()
    {
        Db.Dispose();
    }

    public IAmazonDynamoDB Db { get; private set; }
}


