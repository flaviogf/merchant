using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Merchant
{
    internal class DiscordWriter : IWriter
    {
        private readonly string _webhookURL;

        public DiscordWriter(string webhookURL)
        {
            _webhookURL = webhookURL;
        }

        public void Write(string title, string description, int color, params object[] args)
        {
            Task.Run(async () =>
            {
                try
                {
                    await Execute(title, description, color, args);
                }
                catch
                {
                    // ignored
                }
            });
        }

        private async Task Execute(string title, string description, int color, params object[] args)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(_webhookURL);

                var data = new
                {
                    username = "Merchant",
                    avatar_url = "https://github.com/flaviogf/merchant/raw/master/assets/icon.png",
                    tts = false,
                    embeds = new object[]
                    {
                        new
                        {
                            color = color,
                            title = title,
                            description = description,
                            fields = args
                        }
                    }
                };

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
