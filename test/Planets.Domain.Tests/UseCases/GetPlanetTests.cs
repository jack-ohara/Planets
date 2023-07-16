using System;
using Moq;
using Planets.DataAccessLayer.Repositories;

namespace Planets.Domain.Tests.UseCases
{
    public class GetPlanetTests
    {
        [Fact]
        public async Task GetPlanet_WithAValidID_ShouldMapTheDbObjectToDomainModel()
        {
            var id = "planet-1";
            var mockPlanetsRepository = new Mock<IPlanetRepository>();
            mockPlanetsRepository.Setup(x => x.GetPlanet(id)).ReturnsAsync(
                new DataAccessLayer.Models.Planet
                {
                    ID = "planet-1",
                    Name = "Planet 1",
                    ImageUrl = "https://space-images.com/Planet-1",
                    DistanceToSun = new DataAccessLayer.Models.UnitValuePair
                    {
                        Value = "123",
                        Unit = "km"
                    },
                    Mass = new DataAccessLayer.Models.UnitValuePair
                    {
                        Value = "456",
                        Unit = "kg"
                    },
                    Diameter = new DataAccessLayer.Models.UnitValuePair
                    {
                        Value = "789",
                        Unit = "km"
                    }
                }
            );

            var expected = new Models.Planet
            {
                ID = "planet-1",
                Name = "Planet 1",
                ImageUrl = "https://space-images.com/Planet-1",
                DistanceToSunDisplayValue = "123 km",
                MassDisplayValue = "456 kg",
                DiameterDisplayValue = "789 km",
            };

            var useCase = new GetPlanet(mockPlanetsRepository);

            var result = await useCase.Execute();

            result.Should().BeEquivalentTo(expected);
        }
    }
}

