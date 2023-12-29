using Newtonsoft.Json;
using RestSharp;

namespace MSF.ChatGPT.ChatGPT
{
    public class ChatGPTClient
    {
        private readonly string _apiKey;
        private readonly RestClient _client;
        private string _conversationHistory = string.Empty;

        public ChatGPTClient(string apiKey)
        {
            _apiKey = apiKey;
            _client = new RestClient("https://api.openai.com/v1/engines/text-davinci-003/completions");
        }

        public string SendMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "Sorry, I didn't receive any input. Please try again!";
            }

            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                prompt = message,
                max_tokens = 100,
                n = 1,
                stop = (string?)null,
                temperature = 0.7,
            };

            request.AddJsonBody(JsonConvert.SerializeObject(requestBody));
            var response = _client.Execute(request);

            _conversationHistory += $"User: {message}\n";

            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content ?? string.Empty);

            string chatbotResponse = jsonResponse?.choices[0]?.text?.ToString()?.Trim() ?? string.Empty;

            _conversationHistory += $"Chatbot: {chatbotResponse}\n";
            return chatbotResponse;
        }
    }
}
