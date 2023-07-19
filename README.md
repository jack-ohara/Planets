# Planets

This is a website and accompanying api to give some information on the planets in our solar system.

## Running locally

### Backend

To run the api locally, you must have the follow prerequisites installed:

- [`dotnet` cli](https://learn.microsoft.com/en-us/dotnet/core/tools/)
- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- dynamoDB local (can be run however you like but I will be giving instructions later for using it with Docker)

You need to run dynamoDB on `localhost:8000` and provide the table name as an environment variable. To run dynamo with docker run the following command:

```bash
docker run -d -p 8000:8000 --name my-dynamodb amazon/dynamodb-local
```

From here you can either run the api with the debugger through visual studio or run the following from the root of the project

```bash
dotnet run --project ./planets-api/src/Planets.Api
```

### Frontend

To run the ui locally you will need the following installed:

- node v16 or above
- npm v8 or above

First cd into `planets-ui` and run `npm install` to install all dependencies. Then run `npm run dev` to start the ui.

In order to point at the relevant backend, the ui expects an environment variable named `VITE_APU_URL` to be provided - which you can add through a `.env` file in the root of the api project.

## Deployment

The deployment of the app is managed through the AWS CDK. You first need to produce a production build of the ui before the full stack can be deployed (this would normally be handled in a pipeline)

```bash
cd planets-ui
npm run build # Produces a production bundle that will be uploaded to S3
cd ../planets-api/infra
cdk deploy
```

This will create a lambda function with an API Gateway to host the .net backend and will upload the ui production bundle to s3 and create a cloudfront distribution to serve the ui.
