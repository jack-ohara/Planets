using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Constructs;

namespace Infra
{
    public class PlanetsStack : Stack
    {
        internal PlanetsStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var lambdaFunctionOne = new Function(this, "planets-api", new FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                MemorySize = 1024,
                LogRetention = RetentionDays.ONE_DAY,
                Handler = "Planets.Api",
                Code = Code.FromAsset("../src/", new Amazon.CDK.AWS.S3.Assets.AssetOptions
                {
                    Bundling = new BundlingOptions()
                    {
                        Image = Runtime.DOTNET_6.BundlingImage,
                        User = "root",
                        OutputType = BundlingOutput.ARCHIVED,
                        Command = new string[]{
                            "/bin/sh",
                            "-c",
                            " dotnet tool install -g Amazon.Lambda.Tools"+
                            " && cd Planets.Api"+
                            " && dotnet build"+
                            " && dotnet lambda package --output-package /asset-output/function.zip"
                        }
                    }
                }),
            });

            var restAPI = new LambdaRestApi(this, "planets-proxy-api", new LambdaRestApiProps
            {
                Handler = lambdaFunctionOne,
                Proxy = true,
            });

            var db = new Table(this, "plantes-table", new TableProps
            {
                PartitionKey = new Attribute() { Name = "pk", Type = AttributeType.STRING },
                SortKey = new Attribute() { Name = "sk", Type = AttributeType.STRING },
                TableName = "planets-table",
                BillingMode = BillingMode.PAY_PER_REQUEST
            });

            db.GrantReadWriteData(lambdaFunctionOne);

            new CfnOutput(this, "apigwtarn", new CfnOutputProps { Value = restAPI.ArnForExecuteApi() });
        }
    }
}
