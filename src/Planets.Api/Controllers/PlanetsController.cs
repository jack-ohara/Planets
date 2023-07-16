using Microsoft.AspNetCore.Mvc;
using Planets.Api.Models;
using Planets.Domain.Models;

namespace Planets.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlanetsController : ControllerBase
{
    private readonly ILogger<PlanetsController> _logger;

    public PlanetsController(ILogger<PlanetsController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetPlanets")]
    public IEnumerable<DisplayPlanet> Get()
    {
        return new[] { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
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

