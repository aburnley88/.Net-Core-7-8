namespace WeaatherApp.Models
{
    public class CityWeather
    {
        public string CityCode { get; set; } = string.Empty;
        public string? CityName { get; set; }
        public DateTime DateAndTime { get; set; }
        public int TempFahrenheit { get; set; }
    }
}
