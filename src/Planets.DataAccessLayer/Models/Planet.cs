namespace Planets.DataAccessLayer.Models
{
    public class Planet
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public UnitValuePair DistanceToSun { get; set; }
        public UnitValuePair Mass { get; set; }
        public UnitValuePair Diameter { get; set; }
        public string? AdditionalInfo { get; set; }
    }

    public class UnitValuePair
    {
        public string Value { get; set; }
        public string Unit { get; set; }
    }
}

