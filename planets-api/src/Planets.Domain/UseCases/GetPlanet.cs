using Planets.DataAccessLayer.Repositories;
using Planets.Domain.Models;

namespace Planets.Domain.UseCases
{
    public class GetPlanet : IGetPlanet
    {
        private readonly IPlanetRepository planetRepository;

        public GetPlanet(IPlanetRepository planetRepository)
        {
            this.planetRepository = planetRepository;
        }

        public async Task<Planet?> Execute(string id)
        {
            var planet = await planetRepository.GetPlanet(id);

            return planet is null ? null : new Planet
            {
                ID = planet.ID,
                Name = planet.Name,
                ImageUrl = planet.ImageUrl,
                DistanceToSunDisplayValue = $"{planet.DistanceToSun.Value} {planet.DistanceToSun.Unit}",
                MassDisplayValue = $"{planet.Mass.Value} {planet.Mass.Unit}",
                DiameterDisplayValue = $"{planet.Diameter.Value} {planet.Diameter.Unit}",
                AdditionalInfo = planet.AdditionalInfo
            };
        }
    }
}

