namespace Lab3.ApiCallerConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7139/WeatherForecast");

            using var response = client.Send(request);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }

            Console.ReadLine();
        }
    }
}
