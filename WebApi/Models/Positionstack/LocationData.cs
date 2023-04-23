namespace WebApi.Models.Positionstack
{
    public class LocationData
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
        public string Label { get; set; }
        public string TimezoneString { get; set; }
    }
}
