using BlazorApp1.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherApiCurrentController : ControllerBase
    {

        private readonly ILogger<WeatherApiCurrentController> _logger;
        private readonly IConfiguration _config;

        public WeatherApiCurrentController(ILogger<WeatherApiCurrentController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        [HttpGet("current")]
        public async Task<WeatherApiCurrentDto> Get(HttpClient client)
        {
            var endpoint2 = new StringBuilder();
            endpoint2.Append(_config["WeatherApiUrl"]);
            endpoint2.Append('/');
            endpoint2.Append(_config["WeatherApiVersion"]);
            endpoint2.Append('/');
            endpoint2.Append(_config["CurrentApiPath"]);
            var qs = Request.QueryString;
            // TODO: Replace with config values
            string WeatherApiUrl = "http://api.weatherapi.com";
            string WeatherApiVersion = "v1";
            string apiKey = "";
            string location = "92328";

            string endpoint = $"{WeatherApiUrl}/{WeatherApiVersion}/current.json?key={apiKey}&q={location}&aqi=no";
            return await client.GetFromJsonAsync<WeatherApiCurrentDto>(endpoint);
        }
    }
}
