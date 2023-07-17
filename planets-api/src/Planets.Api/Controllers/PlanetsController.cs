using Microsoft.AspNetCore.Mvc;
using Planets.Api.Models;
using Planets.Domain.UseCases;

namespace Planets.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly IGetAllPlanets getAllPlanets;
    private readonly IGetPlanet getPlanet;
    private readonly ILogger<PlanetsController> logger;

    public PlanetsController(
        IGetAllPlanets getAllPlanets,
        IGetPlanet getPlanet,
        ILogger<PlanetsController> logger
    )
    {
        this.getAllPlanets = getAllPlanets;
        this.getPlanet = getPlanet;
        this.logger = logger;
    }

    [HttpGet(Name = "GetPlanets")]
    public async Task<IActionResult> Get()
    {
        var allPlanets = await getAllPlanets.Execute();

        return Ok(allPlanets.Select(planet => new DisplayPlanet
        {
            ID = planet.ID,
            Name = planet.Name
        }));
    }

    [HttpGet("{id}", Name = "GetPlanet")]
    public async Task<IActionResult> GetPlanet(string id)
    {
        var planet = await getPlanet.Execute(id);

        return planet is null ? NotFound() : Ok(planet);
    }
}

