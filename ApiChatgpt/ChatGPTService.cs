using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiChatgpt
{
    public class ChatGPTService
    {
        private readonly HttpClient _httpClient;

        public ChatGPTService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "https://platform.openai.com/docs/api-reference/introduction");
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "user", content = message }
            }
            };

            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ChatGPTResponse>(jsonResponse);
                return result?.Choices?.FirstOrDefault()?.Message?.Content?.Trim() ?? "No response from ChatGPT.";
            }

            return "Error al conectarse con ChatGPT.";
        }
    }

    public class ChatGPTResponse
    {
        public List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
    }

    public class Message
    {
        public string Content { get; set; }
    }
}

