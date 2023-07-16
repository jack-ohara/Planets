using Planets.Domain.Models;

namespace Planets.Domain.UseCases
{
    public interface IGetPlanet
    {
        Task<Planet?> Execute(string id);
    }
}