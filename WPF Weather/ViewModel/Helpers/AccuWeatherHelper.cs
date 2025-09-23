using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WPF_Weather.Model;

namespace WPF_Weather.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "https://dataservice.accuweather.com/";
        public const string AUTOCOMPLETE_ENDPOINT = "locations/v1/poi/autocomplete?q={0}";
        public const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}";

        // Task represents an ongoing operation when a thread is released from await.
        // When await is used, the operation promises that it will return List<City>, which returns as Task object now.
        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            // construct the url with params
            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, query);

            // using means the internal code will be disposed after the block ends
            using (HttpClient client = new HttpClient())
            {
                // get api key from appsettings.json
                string apiKey = AppConfig.GetApiKey();

                // assign the HTTP Request header value as Bearer with apikey
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    apiKey
                );

                client.BaseAddress = new Uri(url);

                // send a GET request and wait for the server to send back the Header of the response (status)
                var response = await client.GetAsync(url);

                // Download the json string response body from the server
                string json = await response.Content.ReadAsStringAsync();

                // deserialises the json string into List<City> object.
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            // It wasn't disposed as it was declared out using scope.
            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            // construct the url with params
            string url = BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey);

            // using means the internal code will be disposed after the block ends
            using (HttpClient client = new HttpClient())
            {
                // get api key from appsettings.json
                string apiKey = AppConfig.GetApiKey();

                // assign the HTTP Request header value as Bearer with apikey
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    apiKey
                );

                client.BaseAddress = new Uri(url);

                // send a GET request and wait for the server to send back the Header of the response (status)
                var response = await client.GetAsync(url);

                // Download the json string response body from the server
                string json = await response.Content.ReadAsStringAsync();

                // deserialises the json string into List<CurrentConditions> object, use FirstOrDefault to get the first item.
                currentConditions = (
                    JsonConvert.DeserializeObject<List<CurrentConditions>>(json)
                ).FirstOrDefault();
            }

            // It wasn't disposed as it was declared out using scope.
            return currentConditions;
        }
    }
}
