using Planets.DataAccessLayer.Models;

namespace Planets.DataAccessLayer.Tests.TestUtils
{
    public class PlanetBuilder
    {
        private Planet _planet;

        public PlanetBuilder()
        {
            string initialID = Guid.NewGuid().ToString();
            var random = new Random();

            _planet = new Planet
            {
                ID = initialID,
                Name = initialID,
                ImageUrl = $"https://{initialID}.com",
                DistanceToSun = new UnitValuePair
                {
                    Value = random.Next().ToString(),
                    Unit = "km",
                },
                Mass = new UnitValuePair
                {
                    Value = random.Next().ToString(),
                    Unit = "kg"
                },
                Diameter = new UnitValuePair
                {
                    Value = random.Next().ToString(),
                    Unit = "km"
                }
            };
        }

        public Planet Build()
        {
            return _planet;
        }

        public PlanetBuilder WithAdditionalInfo(string additionalInfo)
        {
            _planet.AdditionalInfo = additionalInfo;
            return this;
        }
    }
}

