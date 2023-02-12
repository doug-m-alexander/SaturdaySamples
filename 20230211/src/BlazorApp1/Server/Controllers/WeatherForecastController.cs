using BlazorApp1.Shared;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BlazorApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpGet]
        public IEnumerator<WeatherApiCurrentDTO> Get()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            // TODO: Replace with config values
            string WeatherApiUrl = "http://api.weatherapi.com";
            string WeatherApiVersion = "v1";
            string apiKey = "";
            string location = "92328";

            string endpoint = $"{WeatherApiUrl}/{WeatherApiVersion}/current.json?key={apiKey}&q={location}&aqi=no";
            var response = client.GetAsync(endpoint).Result;
            WeatherApiCurrentDTO? deserializedObject;
            if (response.IsSuccessStatusCode)
            {
                var dataObject = response.Content.ReadAsStringAsync().Result;
                deserializedObject = (WeatherApiCurrentDTO)JsonConvert.DeserializeObject(dataObject, typeof(WeatherApiCurrentDTO));
            }

            return null;
        }
    }
}
