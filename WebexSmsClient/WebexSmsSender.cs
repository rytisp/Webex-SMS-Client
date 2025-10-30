using System.Text;

namespace WebexSmsClient
{
    /// <summary>
    /// Provides functionality for sending SMS messages via the Webex Interact API.
    /// </summary>
    /// <param name="apiKey">Your Webex Interact API key (X-AUTH-KEY).</param>
    /// <param name="fromId">The sender ID registered in your Webex Interact account.</param>
    public class WebexSmsSender(string apiKey, string fromId)
    {
        private readonly string _authKey = apiKey;
        private readonly string _fromId = fromId;
        private static readonly HttpClient client = new();

        /// <summary>
        /// Sends an SMS message asynchronously through the Webex Interact API.
        /// </summary>
        /// <param name="phoneNumberTo">Recipient’s phone number in E.164 format (e.g., +447700900001).</param>
        /// <param name="message">The message body to send.</param>
        /// <param name="endpoint">The Webex Interact SMS API endpoint (e.g., https://api.webexinteract.com/v1/sms/).</param>
        /// <returns>The raw JSON response from the Webex Interact API.</returns>
        /// <exception cref="Exception">Thrown if the SMS send request fails.</exception>
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
