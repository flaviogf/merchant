using Microsoft.Extensions.Logging;

namespace Merchant
{
    public class MerchantLoggerProvider : ILoggerProvider
    {
        private readonly IWriter _writer;

        public MerchantLoggerProvider(MerchantLoggerConfiguration configuration)
        {
            _writer = new DiscordWriter(configuration.BotToken, configuration.ChannelId);
        }

        public ILogger CreateLogger(string name)
        {
            return new MerchantLogger(name, _writer);
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
