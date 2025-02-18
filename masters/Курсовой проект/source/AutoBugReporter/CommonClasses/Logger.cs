using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoBugReporter.CommonClasses
{
    public class Logger
    {
        private static string logFilePath; // Путь к файлу лога
        private static readonly object lockObj = new object(); // Блокировка для многозадачности
        /// <summary>
        /// Инициализация логгера с конфигурацией
        /// </summary>
        /// <param name="logFile"></param>
        public static void SetupLogger(string logFile)
        {
            logFilePath = logFile;
            Console.WriteLine($"Логи будут записываться в файл: {logFilePath}");
        }
        /// <summary>
        /// Метод для записи информации в лог
        /// </summary>
        /// <param name="message"></param>
        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }
        /// <summary>
        /// Метод для записи ошибок в лог
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {
            Log("ERROR", message);
        }
        /// <summary>
        /// Метод для записи ошибок в лог
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }
        /// <summary>
        /// Основной метод записи лога в файл
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        private static void Log(string level, string message)
        {
            try
            {
                // Получаем текущую дату и время для логирования
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Форматируем сообщение
                var logMessage = $"{timestamp} [{level}] {message}";

                // Логируем в файл
                lock (lockObj) // Используем блокировку, чтобы избежать ошибок при многозадачности
                {
                    // Проверяем, существует ли директория для логов
                    var directory = Path.GetDirectoryName(logFilePath);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory); // Если нет, создаем
                    }

                    // Записываем лог в файл
                    using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
                    {
                        writer.WriteLine(logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // Логируем ошибку записи в файл (если это возможно)
                Console.WriteLine($"Ошибка логирования: {ex.Message}");
            }
        }
    }
}
