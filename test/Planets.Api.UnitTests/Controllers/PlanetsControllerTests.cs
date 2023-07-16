using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Planets.Api.Controllers;
using Planets.Domain.Models;
using Planets.Domain.UseCases;

namespace Planets.Api.UnitTests.Controllers
{
    public class PlanetsControllerTests
    {
        [Fact]
        public async Task Get_ShouldReturnMappedPlanets()
        {
            var mockUseCase = new Mock<IGetAllPlanets>();
            mockUseCase.Setup(x => x.Execute()).ReturnsAsync(new[]
            {
                new Planet
                {
                    ID = "planet-1",
                    Name = "Planet 1",
                    ImageUrl = "https://space-images.com/Planet-1",
                    DistanceToSunDisplayValue = "123 km",
                    MassDisplayValue = "456 kg",
                    DiameterDisplayValue = "789 km",
                },
                new Planet
                {
                    ID = "planet-2",
                    Name = "Planet 2",
                    ImageUrl = "https://space-images.com/Planet-2",
                    DistanceToSunDisplayValue = "321 km",
                    MassDisplayValue = "654 kg",
                    DiameterDisplayValue = "987 km",
                    AdditionalInfo = "This planet is very gassy"
                }
            });

            var expected = new[]
            {
                new
                {
                    ID = "planet-1",
                    Name = "Planet 1"
                },
                new
                {
                    ID = "planet-2",
                    Name = "Planet 2"
                },
            };

            var controller = new PlanetsController(
                mockUseCase.Object,
                Mock.Of<IGetPlanet>(),
                Mock.Of<ILogger<PlanetsController>>()
            );

            var result = (await controller.Get()) as OkObjectResult;

            result.Should().NotBeNull();
            result!.Value.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetPlanet_WithAValidID_ShouldReturnTheExpectedPlanet()
        {
            var planet = new Planet
            {
                ID = "planet-1",
                Name = "Planet 1",
                ImageUrl = "https://space-images.com/Planet-1",
                DistanceToSunDisplayValue = "123 km",
                MassDisplayValue = "456 kg",
                DiameterDisplayValue = "789 km",
            };

            var mockUseCase = new Mock<IGetPlanet>();
            mockUseCase.Setup(x => x.Execute(It.IsAny<string>())).ReturnsAsync(planet);

            var controller = new PlanetsController(
                Mock.Of<IGetAllPlanets>(),
                mockUseCase.Object,
                Mock.Of<ILogger<PlanetsController>>()
            );

            var result = await controller.GetPlanet("planet-1");

            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(planet);
        }

        [Fact]
        public async Task GetPlanet_WithAnInvalidID_ShouldReturnANotFoundResult()
        {
            var mockUseCase = new Mock<IGetPlanet>();
            mockUseCase.Setup(x => x.Execute(It.IsAny<string>())).ReturnsAsync(() => null);

            var controller = new PlanetsController(
                Mock.Of<IGetAllPlanets>(),
                mockUseCase.Object,
                Mock.Of<ILogger<PlanetsController>>()
            );

            var result = await controller.GetPlanet("id");

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
