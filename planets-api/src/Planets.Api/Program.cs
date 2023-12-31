﻿using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Planets.DataAccessLayer.Repositories;
using Planets.Domain.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

builder.Services.AddScoped<IGetAllPlanets, GetAllPlanets>();
builder.Services.AddScoped<IGetPlanet, GetPlanet>();
builder.Services.AddScoped<IPlanetRepository>(sp => new PlanetRepository(sp.GetRequiredService<IAmazonDynamoDB>(), Environment.GetEnvironmentVariable("TABLE_NAME")));

if (builder.Environment.IsDevelopment())
{
    var config = new AmazonDynamoDBConfig();
    config.RegionEndpoint = RegionEndpoint.EUWest1;
    config.ServiceURL = "http://localhost:8000";
    builder.Services.AddScoped<IAmazonDynamoDB>(sp => new AmazonDynamoDBClient(new BasicAWSCredentials("myFakeAccessKey", "myFakeSecretKey"), config));
}
else
{
    builder.Services.AddScoped<IAmazonDynamoDB, AmazonDynamoDBClient>();
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseCors("AllowAllOrigins");
