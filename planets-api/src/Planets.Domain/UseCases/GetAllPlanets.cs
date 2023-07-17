using Planets.DataAccessLayer.Repositories;
using Planets.Domain.Models;

namespace Planets.Domain.UseCases
{
    public class GetAllPlanets : IGetAllPlanets
    {
        private readonly IPlanetRepository planetRepository;

        public GetAllPlanets(IPlanetRepository planetRepository)
        {
            this.planetRepository = planetRepository;
        }

        public async Task<IEnumerable<Planet>> Execute()
        {
            var allPlanets = await planetRepository.GetAllPlanets();

            return allPlanets.Select(planet => new Planet
            {
                ID = planet.ID,
                Name = planet.Name,
                ImageUrl = planet.ImageUrl,
                DistanceToSunDisplayValue = $"{planet.DistanceToSun.Value} {planet.DistanceToSun.Unit}",
                MassDisplayValue = $"{planet.Mass.Value} {planet.Mass.Unit}",
                DiameterDisplayValue = $"{planet.Diameter.Value} {planet.Diameter.Unit}",
                AdditionalInfo = planet.AdditionalInfo
            });
        }
    }
}

