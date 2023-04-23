using static WebApi.Utils.CommonUtils;

namespace WebApi.Models.SunriseSunset;

public class SunriseSunset
{
    public TimeOnly Sunrise { get; set; }
    public TimeOnly Sunset { get; set; }
    public TimeOnly FirstLight { get; set; }
    public TimeOnly LastLight { get; set; }
    public TimeOnly Dawn { get; set; }
    public TimeOnly Dusk { get; set; }
    public TimeOnly SolarNoon { get; set; }
    public TimeOnly GoldenHour { get; set; }
    public TimeOnly DayLength { get; set; }
    public Timezone Timezone { get; set; }
    public string DateString { get; set; }
}
