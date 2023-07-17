using Planets.DataAccessLayer.Models;

namespace Planets.DataAccessLayer.Repositories
{
    public interface IPlanetRepository
    {
        Task<IEnumerable<Planet>> GetAllPlanets();
        Task<Planet?> GetPlanet(string id);
    }
}