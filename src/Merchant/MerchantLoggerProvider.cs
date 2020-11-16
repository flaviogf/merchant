using Microsoft.Extensions.Logging;

namespace Merchant
{
    public class MerchantLoggerProvider : ILoggerProvider
    {
        private readonly MerchantLoggerConfiguration _configuration;

        public MerchantLoggerProvider(MerchantLoggerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ILogger CreateLogger(string name)
        {
            var writer = new DiscordWriter(_configuration.BotToken, _configuration.ChannelId);

            return new MerchantLogger(name, writer);
        }

        public void Dispose()
        {
        }
    }
}
