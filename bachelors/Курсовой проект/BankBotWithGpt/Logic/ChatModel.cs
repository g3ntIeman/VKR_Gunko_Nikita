using AI.Dev.OpenAI.GPT;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Data.Analysis;
using Microsoft.Extensions.Configuration;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using System.Diagnostics;

namespace BankBotWithGpt.Logic
{
    public class ChatModel
    {
        private static IOpenAIService? openAIService;
        private static string basePath = "";
        public ChatModel()
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            var apiKey = config.GetSection("OpenAIServiceOptions:ApiKey").Value;
            basePath = config.GetSection("FileStorageOptions:BasePath").Value;

            if (String.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("Ошибка! Не задан OpenAI API ключ.");
                return;
            }

            openAIService = new OpenAIService(new OpenAiOptions()
            {
                ApiKey = apiKey
            });
        }

        private static double[] DistancesFromEmbeddings(IEnumerable<double> qEmbeddings, IEnumerable<IEnumerable<double>> embeddings, string distanceMetric = "cosine")
        {

            Vector<double> questionVector = Vector<double>.Build.DenseOfArray(qEmbeddings.ToArray());

            int numEmbeddings = embeddings.Count();
            double[] distances = new double[numEmbeddings];

            int index = 0;
            foreach (var embedding in embeddings)
            {
                double[] currentEmbedding = embedding.ToArray();
                Vector<double> currentVector = Vector<double>.Build.DenseOfArray(currentEmbedding);

                if (distanceMetric == "cosine")
                {
                    double cosineDistance = 1 - (questionVector * currentVector) / (questionVector.L2Norm() * currentVector.L2Norm());
                    distances[index++] = cosineDistance;
                }
                else
                {
                    throw new ArgumentException("Unsupported distance metric");
                }
            }

            return distances;
        }
        /// <summary>
        /// Метод для загрузки предобработанных данных
        /// </summary>
        /// <returns></returns>
        public static DataFrame GetEmbeddings()
        {
            string csvFilePath = Path.Combine(basePath, @"Processed\sber_data_embeddings.csv");
            DataFrame df = new DataFrame();

            try
            {
                df = DataFrame.LoadCsv(csvFilePath, separator: '|');
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! Не удалось загрузить файл с эмбеддингами: \n" + ex.ToString());
                return null;
            }

            return df;
        }

        public static async Task<string> CreateContext(string question, DataFrame df, int maxLen = 3500, string size = "ada")
        {
            List<List<double>> embeddings = new List<List<double>>();

            foreach (var row in df.Rows)
            {
                string[] strEmbeddings = row[df.Columns.IndexOf("embedding")].ToString().Trim('[', ']').Split(",");
                List<double> embedding = new List<double>();
                foreach (var embed in strEmbeddings)
                {
                    embedding.Add(double.Parse(embed, System.Globalization.CultureInfo.InvariantCulture));
                }
                embeddings.Add(embedding);
            }

            var response = await openAIService.Embeddings.CreateEmbedding(new EmbeddingCreateRequest()
            {
                Input = question,
                Model = "text-embedding-ada-002"
            });

            if (!response.Successful)
            {
                Debug.WriteLine(response?.Error?.Message);
                return string.Empty;
            }

            var qEmbeddings = response.Data[0].Embedding;

            double[] distances = DistancesFromEmbeddings(qEmbeddings, embeddings);

            var distancesColumn = new PrimitiveDataFrameColumn<double>("distances", distances);
            df.Columns.Add(distancesColumn);

            DataFrame sortedDf = df.OrderBy("distances");

            List<string> returns = new List<string>();
            int curLen = 0;

            var tokenCounts = sortedDf.Columns["text"].Cast<string>().Select(t => GPT3Tokenizer.Encode(t).Count).ToArray();
            // Add the token counts to the DataFrame
            var tokenCountsColumn = new PrimitiveDataFrameColumn<int>("n_tokens", tokenCounts);
            sortedDf.Columns.Add(tokenCountsColumn);

            foreach (DataFrameRow row in sortedDf.Rows)
            {
                curLen += int.Parse(row[sortedDf.Columns.IndexOf("n_tokens")].ToString()) + 4;

                if (curLen > maxLen)
                {
                    break;
                }

                returns.Add(row[sortedDf.Columns.IndexOf("text")].ToString());
            }

            return string.Join("\n", returns);
        }
        public static async Task<string> AnswerQuestion(
            string question,
            string model = "gpt-3.5-turbo",
            int maxLen = 3500,
            string size = "ada",
            bool debug = true,
            int maxTokens = 500,
            string stopSequence = null)
        {

            string context = await CreateContext(question, GetEmbeddings(), maxLen, size);

            try
            {
                var prompt = (String.IsNullOrEmpty(context) ? "" : $"Используйте приведенный ниже контекст, чтобы ответить на вопрос. Если ты не можешь сформировать ответ, основываясь на контексте, напиши \"Обратитесь в ближайшее отделение банка для уточнения этого вопроса.\"\n\nКонтекст: \n---\n{context}\n---\n\n") + $"Вопрос: {question}\nОтвет:";

                if (debug)
                {
                    Debug.WriteLine("Prompt:\n" + prompt);
                    Debug.WriteLine("\n\n");
                }

                var completionRequest = new ChatCompletionCreateRequest()
                {
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem("Ты выполняешь обязанности консультанта по банковским услугам банка СберБанк. Ты строго следуешь правилам, описанным в сообщении. Ты не отвечаешь на вопросы, не связанные с банковской темой."),
                        //ChatMessage.FromAssistant(context),
                        ChatMessage.FromUser(prompt)
                    },
                    Temperature = 0,
                    MaxTokens = maxTokens,
                    TopP = 1,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                    StopAsList = stopSequence != null ? new[] { stopSequence } : null,
                    Model = Models.ChatGpt3_5Turbo
                };

                var completionResponse = await openAIService.ChatCompletion.CreateCompletion(completionRequest);
                return completionResponse.Choices[0].Message.Content;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return "";
            }
        }
    }
}
