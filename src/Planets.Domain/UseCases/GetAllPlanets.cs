﻿using Planets.DataAccessLayer.Repositories;
using Planets.Domain.Models;

namespace Planets.Domain.UseCases
{
	public class GetAllPlanets
	{
        private readonly IPlanetRepository planetRepository;

        public GetAllPlanets(IPlanetRepository planetRepository)
		{
            this.planetRepository = planetRepository;
        }

        public async Task<IEnumerable<Planet>> Execute()
        {
            var allPlanets = await planetRepository.GetAllPlanets();

            // We don't need to return all this for the GetAll endpoint,
            // UI will only care about the ID and name (maybe image depending
            // on implementation). Is that a controller or domain concern??
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

