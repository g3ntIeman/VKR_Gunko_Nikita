using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoBugReporter.CommonClasses;
using AutoBugReporter.ModelClasses;

namespace AutoBugReporter.Services
{
    public class IntradescClient
    {
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public IntradescClient(string apiKey, string apiUrl)
        {
            _apiKey = apiKey;
            _apiUrl = apiUrl;
        }
        /// <summary>
        /// Получение списка заявок
        /// </summary>
        /// <returns></returns>
        public async Task<List<Ticket>> GetTicketsAsync()
        {
            string url = $"{_apiUrl}?ApiKey={_apiKey}&$top=5";
            return await HttpHelper.SendRequestAsync<List<Ticket>>(HttpMethod.Get, url);
        }
        /// <summary>
        /// Создание заявки
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CreateTicketAsync(CreateTicketRequest request)
        {
            string url = $"{_apiUrl}?ApiKey={_apiKey}";
            var response = await HttpHelper.SendRequestAsync<object>(HttpMethod.Post, url, request);
            return response != null;
        }
        /// <summary>
        /// Изменение заявки
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTicketAsync(UpdateTicketRequest request)
        {
            string url = $"{_apiUrl}?ApiKey={_apiKey}";
            var response = await HttpHelper.SendRequestAsync<object>(HttpMethod.Put, url, request);
            return response != null;
        }
        /// <summary>
        /// Добавление комментария
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddCommentAsync(AddCommentRequest request)
        {
            string url = $"{_apiUrl}?ApiKey={_apiKey}";
            var response = await HttpHelper.SendRequestAsync<object>(HttpMethod.Put, url, request);
            return response != null;
        }
    }
}
