using System;
using System.IO;
using System.Text.Json;

namespace AutoBugReporter.CommonClasses
{
    public class AppSettings
    {
        public ApiKeysSection ApiKeys { get; set; }
        public ApiUrlsSection ApiUrls { get; set; }
        public SchedulerSection Scheduler { get; set; }
        public LoggingSection Logging { get; set; }
    }

    public class ApiKeysSection
    {
        public string INTRADESC { get; set; }
        public string OpenAI { get; set; }
    }

    public class ApiUrlsSection
    {
        public string INTRADESC { get; set; }
    }

    public class SchedulerSection
    {
        public int PollIntervalMinutes { get; set; }
    }

    public class LoggingSection
    {
        public string LogFilePath { get; set; }
    }

    public class ConfigurationService
    {
        public static AppSettings LoadConfiguration()
        {
            Console.Write("Введите путь до конфигурационного файла (например, config.json): ");
            var configFilePath = Console.ReadLine();

            if (!File.Exists(configFilePath))
            {
                Console.WriteLine("Ошибка: файл конфигурации не найден!");
                return null;
            }

            try
            {
                string json = File.ReadAllText(configFilePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var config = JsonSerializer.Deserialize<AppSettings>(json, options);

                Console.WriteLine("Конфигурация загружена успешно!");
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки конфигурации: {ex.Message}");
                return null;
            }
        }
    }
}
