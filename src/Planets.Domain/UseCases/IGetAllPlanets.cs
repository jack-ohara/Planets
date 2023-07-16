using Planets.Domain.Models;

namespace Planets.Domain.UseCases
{
    public interface IGetAllPlanets
    {
        Task<IEnumerable<Planet>> Execute();
    }
}