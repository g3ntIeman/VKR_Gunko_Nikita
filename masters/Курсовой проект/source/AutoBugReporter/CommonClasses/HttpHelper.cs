using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AutoBugReporter.CommonClasses
{
    public static class HttpHelper
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<T> SendRequestAsync<T>(HttpMethod method, string url, object body = null, string apiKey = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, url);

                if (!string.IsNullOrEmpty(apiKey))
                    request.Headers.Add("ApiKey", apiKey);

                if (body != null)
                {
                    string json = JsonSerializer.Serialize(body);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка HTTP-запроса: {ex.Message}");
                return default;
            }
        }
    }
}
