namespace pdf_microservice.Models;

public class GeoArgument
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.Now;
}