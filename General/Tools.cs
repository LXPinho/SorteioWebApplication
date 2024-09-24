using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SorteioWebApplication.Tools
{
    public class Tools
    {
        static public HttpClient? _HttpClient { get; set; } = null; // new HttpClient();
        static public IConfigurationRoot ConfigurationBuilder { get; set; } 
            = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        public static HttpClient getInstanceHttpClient()
        {
            try
            {
                if (_HttpClient == null)
                {

                    string uri =
                        ConfigurationBuilder
                            .GetSection("WebApplicationSettings")
                            .GetSection("Parameters")
                            .GetValue<string>("Uri") ?? string.Empty;

                    // Update port # in the following line.
                    _HttpClient = new HttpClient();
                    _HttpClient.BaseAddress = new Uri(uri.ToLower());
                    _HttpClient.DefaultRequestHeaders.Accept.Clear();
                    _HttpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }
            }
            catch
            {
                _HttpClient = null;
            }
            return _HttpClient ?? new HttpClient(); ;
        }
        public static string GetRequestUrl()
        {
            string api =
                ConfigurationBuilder
                    .GetSection("WebApplicationSettings")
                    .GetSection("Parameters")
                    .GetValue<string>("RequestUrl") ?? string.Empty;

            return api;
        }
        public static int GetVibesAcumulados()
        {
            int result = 0;

            string vibesAcumulados =
            ConfigurationBuilder
                .GetSection("WebApplicationSettings")
                .GetSection("Parameters")
                .GetValue<string>("VibesAcumulados") ?? string.Empty;

            int.TryParse(vibesAcumulados, out result);

            return result;
        }
    }
}
