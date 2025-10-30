using System.Text;

namespace WebexSmsClient
{
    public class WebexSmsSender(string apiKey, string fromId)
    {
        private readonly string _authKey = apiKey;
        private readonly string _fromId = fromId;
        private static readonly HttpClient client = new();

        public async Task<string> SendSmsAsync(string phoneNumberTo, string message, string endpoint)
        {
            var jsonBody = $@"{{
                ""from"": ""{_fromId}"",
                ""to"": [{{ ""phone"": [""{phoneNumberTo}""] }}],
                ""message_body"": ""{message.Replace("\"", "\\\"")}""
            }}";

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Add("X-AUTH-KEY", _authKey);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"SMS send failed ({response.StatusCode}): {responseBody}");

            return responseBody;
        }
    }
}
