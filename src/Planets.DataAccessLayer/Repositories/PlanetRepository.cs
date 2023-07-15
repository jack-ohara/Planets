﻿using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Planets.DataAccessLayer.Models;

namespace Planets.DataAccessLayer.Repositories;

public class PlanetRepository
{
    private Table _table;

    public PlanetRepository(IAmazonDynamoDB client, string tableName)
    {
        _table = Table.LoadTable(client, tableName);
    }

    public async Task<IEnumerable<Planet>> GetAllPlanets()
    {
        var query = new QueryFilter("pk", QueryOperator.Equal, "planet/list");

        var search = _table.Query(query);

        List<Document> documentSet;
        var planets = new List<Planet>();
        do
        {
            documentSet = await search.GetNextSetAsync();

            foreach (var document in documentSet)
            {
                var distanceToSun = document["DistanceToSun"].AsDocument();
                var mass = document["Mass"].AsDocument();
                var diameter = document["Diameter"].AsDocument();
                var additionalInfo = document.TryGetValue("AdditionalInfo", out var x) ? x : null;

                var planet = new Planet
                {
                    ID = document["ID"],
                    Name = document["Name"],
                    ImageUrl = document["ImageUrl"],
                    DistanceToSun = new UnitValuePair
                    {
                        Value = distanceToSun["Value"],
                        Unit = distanceToSun["Unit"]
                    },
                    Mass = new UnitValuePair
                    {
                        Value = mass["Value"],
                        Unit = mass["Unit"]
                    },
                    Diameter = new UnitValuePair
                    {
                        Value = diameter["Value"],
                        Unit = diameter["Unit"]
                    },
                };

                if (additionalInfo != null)
                    planet.AdditionalInfo = additionalInfo;

                planets.Add(planet);
            }
        } while (!search.IsDone);

        return planets;
    }
}
