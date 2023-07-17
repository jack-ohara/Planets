using FluentAssertions;
using Moq;
using Planets.DataAccessLayer.Repositories;
using Planets.Domain.UseCases;

namespace Planets.Domain.UnitTests.UseCases
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

            var useCase = new GetPlanet(mockPlanetsRepository.Object);

            var result = await useCase.Execute(id);

            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetPlanet_WithAnInvalidID_ShouldReturnNull()
        {
            var mockPlanetsRepository = new Mock<IPlanetRepository>();
            var useCase = new GetPlanet(mockPlanetsRepository.Object);

            var result = await useCase.Execute("does-not-exist");

            result.Should().BeNull();
        }
    }
}

