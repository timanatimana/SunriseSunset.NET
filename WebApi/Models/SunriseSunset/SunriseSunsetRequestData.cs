using static WebApi.Utils.CommonUtils;

namespace WebApi.Models.SunriseSunset
{
    public class SunriseSunsetRequestData
    {
        public Coordinates Coordinates { get; set; }
        public string DateString { get; set; }
        public Timezone Timezone { get; set; }
    }
}
