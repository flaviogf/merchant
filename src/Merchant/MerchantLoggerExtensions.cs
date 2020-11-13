using System;
using Microsoft.Extensions.Logging;

namespace Merchant
{
    public static class MerchantLoggerExtensions
    {
        public static ILoggingBuilder AddMerchant(this ILoggingBuilder builder, Action<MerchantLoggerConfiguration> action)
        {
            var config = new MerchantLoggerConfiguration();

            action(config);

            if (string.IsNullOrEmpty(config.BotToken))
            {
                throw new ArgumentException($"{nameof(config.BotToken)} must be informed");
            }

            if (string.IsNullOrEmpty(config.ChannelId))
            {
                throw new ArgumentException($"{nameof(config.ChannelId)} must be informed");
            }

            builder.AddProvider(new MerchantLoggerProvider(config));

            return builder;
        }
    }
}
