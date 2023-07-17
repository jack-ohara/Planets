namespace Planets.Domain.Models;

public class Planet
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public string DistanceToSunDisplayValue { get; set; }
    public string MassDisplayValue { get; set; }
    public string DiameterDisplayValue { get; set; }
    public string? AdditionalInfo { get; set; }
}
