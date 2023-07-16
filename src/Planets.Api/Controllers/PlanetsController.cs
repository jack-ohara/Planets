using Microsoft.AspNetCore.Mvc;
using Planets.Api.Models;
using Planets.Domain.Models;
using Planets.Domain.UseCases;

namespace Planets.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly IGetAllPlanets getAllPlanets;
    private readonly ILogger<PlanetsController> logger;

    public PlanetsController(
        IGetAllPlanets getAllPlanets,
        ILogger<PlanetsController> logger
    )
    {
        this.getAllPlanets = getAllPlanets;
        this.logger = logger;
    }

    [HttpGet(Name = "GetPlanets")]
    public async Task<IEnumerable<DisplayPlanet>> Get()
    {
        var allPlanets = await getAllPlanets.Execute();

        return allPlanets.Select(planet => new DisplayPlanet
        {
            ID = planet.ID,
            Name = planet.Name
        });
    }

    [HttpGet("{id}", Name = "GetPlanet")]
    public Planet GetPlanet(string id)
    {
        return new Planet()
        {
            Name = id
        };
    }
}

