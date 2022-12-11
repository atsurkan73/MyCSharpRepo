using AsynchronousCalls;
using Newtonsoft.Json;

public class ApiRequest
{
    public async Task<IEnumerable<Weather>> GetData()
    {
        List<Weather> weathers = new List<Weather>();

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://open-weather13.p.rapidapi.com/city/kyiv"),
            Headers =
    {
        { "X-RapidAPI-Key", "aebe14f1f5msh9e225e9abc0b8c6p1140d9jsne009b8fbb2cc" },
        { "X-RapidAPI-Host", "open-weather13.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            
            Console.WriteLine("Printout raw json data:");
            Console.WriteLine(body);

            var weatherList = JsonConvert.DeserializeObject(body);

            Console.WriteLine("Printout data after convertion:");
            Console.WriteLine(weatherList);
        }

        return weathers;
    }
}








