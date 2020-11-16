using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Merchant
{
    internal class DiscordWriter : IWriter
    {
        private readonly string _botToken;

        private readonly string _channelId;

        public DiscordWriter(string botToken, string channelId)
        {
            _botToken = botToken;

            _channelId = channelId;
        }

        public void Write(string title, string subtitle, string description)
        {
            Task.Run(async () =>
            {
                try
                {
                    await OpenConnection();

                    await Execute(title, subtitle, description);
                }
                catch
                {
                    // ignored
                }
            });
        }

        private async Task OpenConnection()
        {
            using (var client = new ClientWebSocket())
            {
                var uri = new Uri("wss://gateway.discord.gg/?v=6&encoding=json");

                await client.ConnectAsync(uri, CancellationToken.None);

                var data = new
                {
                    op = 2,
                    d = new
                    {
                        token = _botToken,
                        intents = 513
                    }
                };

                var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));

                await client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        private async Task Execute(string title, string subtitle, string description)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri($"https://discord.com/api/v6/channels/{_channelId}/messages");

                client.DefaultRequestHeaders.Add("Authorization", $"Bot {_botToken}");

                var data = new
                {
                    content = title,
                    tts = false,
                    embed = new
                    {
                        title = subtitle,
                        description = description
                    }
                };

                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(uri, content);

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
