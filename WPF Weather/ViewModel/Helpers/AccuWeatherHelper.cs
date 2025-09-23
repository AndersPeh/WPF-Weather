using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Weather.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "https://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/poi/autocomplete?q={0}";

        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BASE_URL);
            string apiKey = AppConfig.GetApiKey();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                apiKey
            );
            return client;
        }
    }
}
