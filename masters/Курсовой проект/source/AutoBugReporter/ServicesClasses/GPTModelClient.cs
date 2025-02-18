using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoBugReporter.CommonClasses;

namespace AutoBugReporter.Services
{
    public class GPTModelClient
    {
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://api.openai.com/v1/completions";

        public GPTModelClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GenerateDescriptionAsync(string ticketDescription)
        {
            var prompt = $"Опиши проблему: {ticketDescription}\nЧто делаем? Что получаем? Что должны получить? Почему этого не происходит?";

            var requestBody = new
            {
                model = "gpt-4",
                prompt = prompt,
                max_tokens = 150
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            try
            {
                string json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiUrl, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<dynamic>(responseBody);

                return result?.choices[0]?.text?.ToString() ?? "Ошибка генерации описания";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе к OpenAI: {ex.Message}");
                return "Ошибка обработки запроса";
            }
        }
    }
}
