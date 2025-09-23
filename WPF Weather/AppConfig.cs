using Microsoft.Extensions.Configuration;

namespace WPF_Weather
{
    public static class AppConfig
    {
        public static IConfigurationRoot Configuration { get; }

        static AppConfig()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetApiKey()
        {
            return Configuration["ApiKey"];
        }
    }
}
