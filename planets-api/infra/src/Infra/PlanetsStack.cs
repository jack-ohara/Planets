using System.Collections.Generic;
using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Deployment;
using Amazon.CDK.AWS.CloudFront;
using Amazon.CDK.AWS.CloudFront.Origins;
using Constructs;

namespace Infra
{
    public class PlanetsStack : Stack
    {
        internal PlanetsStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            var dynamoTable = new Table(this, "plantes-table", new TableProps
            {
                PartitionKey = new Attribute() { Name = "pk", Type = AttributeType.STRING },
                SortKey = new Attribute() { Name = "sk", Type = AttributeType.STRING },
                TableName = "planets-table",
                BillingMode = BillingMode.PAY_PER_REQUEST
            });

            var planetsApiLambda = new Amazon.CDK.AWS.Lambda.Function(this, "planets-api", new Amazon.CDK.AWS.Lambda.FunctionProps
            {
                Runtime = Runtime.DOTNET_6,
                MemorySize = 1024,
                LogRetention = RetentionDays.ONE_DAY,
                Handler = "Planets.Api",
                Environment = new Dictionary<string, string>()
                {
                    ["TABLE_NAME"] = dynamoTable.TableName
                },
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

            dynamoTable.GrantReadData(planetsApiLambda);

            // I ended up having to add a HTTP api manually through the console
            // because of a CORS error I didn't have time to properly understand

            //var restAPI = new LambdaRestApi(this, "planets-proxy-api", new LambdaRestApiProps
            //{
            //    Handler = lambdaFunctionOne,
            //    Proxy = true,
            //    DefaultCorsPreflightOptions = new CorsOptions
            //    {
            //        AllowOrigins = Cors.ALL_ORIGINS,
            //        AllowMethods = Cors.ALL_METHODS,
            //        AllowCredentials = true,
            //    },
            //});

            //new CfnOutput(this, "apigwarn", new CfnOutputProps { Value = restAPI.ArnForExecuteApi() });

            var uiS3Bucket = new Bucket(this, "planets-ui-bucket", new BucketProps
            {
                AccessControl = BucketAccessControl.PRIVATE
            });

            new BucketDeployment(this, "planets-ui-bucket-deployment", new BucketDeploymentProps
            {
                DestinationBucket = uiS3Bucket,
                Sources = new[] { Source.Asset("../../planets-ui/dist") }
            });

            var originAccessIdentity = new OriginAccessIdentity(this, "origin-access-identity");
            uiS3Bucket.GrantRead(originAccessIdentity);

            new Distribution(this, "planets-cloudfront-distribution", new DistributionProps
            {
                DefaultRootObject = "index.html",
                DefaultBehavior = new BehaviorOptions
                {
                    Origin = new S3Origin(uiS3Bucket, new S3OriginProps { OriginAccessIdentity = originAccessIdentity })
                } 
            });
        }
    }
}
