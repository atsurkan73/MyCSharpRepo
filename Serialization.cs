using System.Text.Json;
using static Calendar.Meeting;

namespace AsynchronousCalls;

public static class Serialization
{
    public static Weather DeserializeWeather(string jsonBody)
    {
        var weatherData = JsonSerializer
            .Deserialize<Weather>(jsonBody);
        return weatherData;
    }
}


