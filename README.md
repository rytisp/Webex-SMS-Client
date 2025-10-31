# WebexSmsClient

A lightweight C# library for sending SMS messages through the **[Webex Interact API](https://docs.webexinteract.com/reference/sms-api)**.

This client provides a simple async method to send SMS messages with minimal setup, suitable for use in alerts, notifications, and automation systems.

---

## 🚀 Installation

Install from **NuGet**:

    dotnet add package WebexSmsClient

or using Visual Studio Package Manager:

    PM> Install-Package WebexSmsClient

---

## 💡 Usage Example

    using System;
    using System.Threading.Tasks;
    using WebexSmsClient;

    class Program
    {
        static async Task Main(string[] args)
        {
            var smsClient = new WebexSmsSender(
                apiKey: "YOUR_X_AUTH_KEY",
                fromId: "YourFromID"
            );

            string recipient = "+447700900001";
            string message = "Your appointment on 29th October at 12:30PM.";
            string endpoint = "https://api.webexinteract.com/v1/sms/";

            try
            {
                var response = await smsClient.SendSmsAsync(recipient, message, endpoint);
                Console.WriteLine("Message sent successfully!");
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send SMS: {ex.Message}");
            }
        }
    }

---

## 💡 Usage Example Simplified

    using WebexSmsClient;

    class Program
    {
        static async Task Main(string[] args)
        {
            var smsClient = new WebexSmsSender(apiKey: "YOUR_X_AUTH_KEY", fromId: "YourFromID");
            var response = await smsClient.SendSmsAsync("+447700900001", "Hello. Booking confirmed.", "https://api.webexinteract.com/v1/sms/");
        }
    }

---

## ⚙️ Method Reference

### SendSmsAsync(string phoneNumberTo, string message, string endpoint)

| Parameter | Type | Description |
|------------|------|-------------|
| phoneNumberTo | string | The recipient’s phone number in E.164 format (e.g., +447700900001). |
| message | string | The SMS message body text. |
| endpoint | string | The Webex Interact API endpoint (e.g., https://api.webexinteract.com/v1/sms/). |

Returns: Task<string> — the raw JSON response from the Webex API.
Throws: Exception if the HTTP response status is not successful.

---

## 🌍 Example Endpoints

| Environment | Endpoint |
|--------------|-----------|
| Production | https://api.webexinteract.com/v1/sms/ |
| Sandbox/Test | https://api.webexinteract.com/v1/sms/test |

---

## 🧩 Example Use Cases

- IoT or power outage alerts
- System or device notifications
- Two-factor authentication or verification codes
- Customer messaging or status updates

---

## 🪪 License

Licensed under the MIT License.
