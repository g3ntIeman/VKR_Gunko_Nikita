using AutoBugReporter.CommonClasses;
using AutoBugReporter.ModelClasses;
using AutoBugReporter.Services;

namespace AutoBugReporter
{
    class Program
    {
        /// <summary>
        /// Главная функция программы
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            // Загружаем конфигурацию
            var config = ConfigurationService.LoadConfiguration();

            if (config == null)
            {
                Console.WriteLine("Ошибка загрузки конфигурации.");
                return;
            }

            // Инициализация клиентов
            var intradescClient = new IntradescClient(config.ApiKeys.INTRADESC, config.ApiUrls.INTRADESC);
            var openAiClient = new GPTModelClient(config.ApiKeys.OpenAI);

            // Получаем заявки
            var tickets = await intradescClient.GetTicketsAsync();
            if (tickets != null)
            {
                Console.WriteLine($"Найдено заявок: {tickets.Count}");
                foreach (var ticket in tickets)
                {
                    Console.WriteLine($"[{ticket.Id}] {ticket.Title}");
                }
            }

            // Создаем заявку
            var newTicket = new CreateTicketRequest
            {
                Blocks = new TicketBlocks
                {
                    Name = new ValueWrapper<string>("Ошибка в системе"),
                    Description = new ValueWrapper<string>("Программа не отвечает"),
                    Tags = new ValueWrapper<List<int>>(new List<int> { 33290 })
                }
            };

            bool created = await intradescClient.CreateTicketAsync(newTicket);
            Console.WriteLine(created ? "Заявка успешно создана!" : "Ошибка создания заявки.");

            // 3️⃣ Генерация описания через OpenAI
            string aiGeneratedText = await openAiClient.GenerateDescriptionAsync("Программа зависает при запуске");
            Console.WriteLine($"GPT: {aiGeneratedText}");
        }
    }
}