using Moq;
using Planets.DataAccessLayer.Repositories;

namespace Planets.Domain.Tests.UseCases
{
	public class GetAllPlanetsTests
	{
		[Fact]
		public async Task GetAllPlanets_ShouldMapDbObjectsToDomainModels()
		{
			var mockPlanetsRepository = new Mock<IPlanetRepository>();
			mockPlanetsRepository.Setup(x => x.GetAllPlanets()).ReturnsAsync(new[]
			{
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
				},
				new DataAccessLayer.Models.Planet
				{
					ID = "planet-2",
					Name = "Planet 2",
					ImageUrl = "https://space-images.com/Planet-2",
					DistanceToSun = new DataAccessLayer.Models.UnitValuePair
					{
						Value = "321",
						Unit = "km"
					},
					Mass = new DataAccessLayer.Models.UnitValuePair
					{
						Value = "654",
						Unit = "kg"
					},
					Diameter = new DataAccessLayer.Models.UnitValuePair
					{
						Value = "987",
						Unit = "km"
					},
					AdditionalInfo = "This planet is very gassy"
				},
			});

			var expected = new[]
			{
				new Models.Planet
				{
					ID = "planet-1",
					Name = "Planet 1",
					ImageUrl = "https://space-images.com/Planet-1",
					DistanceToSunDisplayValue = "123 km",
					MassDisplayValue = "456 kg",
					DiameterDisplayValue = "789 km",
				},
				new Models.Planet
				{
					ID = "planet-2",
					Name = "Planet 2",
					ImageUrl = "https://space-images.com/Planet-2",
					DistanceToSunDisplayValue = "321 km",
					MassDisplayValue = "654 kg",
					DiameterDisplayValue = "987 km",
                    AdditionalInfo = "This planet is very gassy"
                },
			};

			var useCase = new GetAllPlanets(mockPlanetsRepository.Object);

			var result = await useCase.Execute();

			result.Should().BeEquivalentTo(expected);
		}
	}
}

