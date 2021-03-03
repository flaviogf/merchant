using System;
using Microsoft.Extensions.Logging;

namespace Merchant
{
    public static class MerchantLoggerExtensions
    {
        public static ILoggingBuilder AddMerchant(this ILoggingBuilder builder, MerchantLoggerConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentException($"{nameof(config)} must be informed");
            }

            if (string.IsNullOrEmpty(config.WebhookURL))
            {
                throw new ArgumentException($"{nameof(config.WebhookURL)} must be informed");
            }

            builder.AddProvider(new MerchantLoggerProvider(config));

            return builder;
        }

        public static ILoggingBuilder AddMerchant(this ILoggingBuilder builder, Action<MerchantLoggerConfiguration> action)
        {
            var config = new MerchantLoggerConfiguration();

            action(config);

            if (string.IsNullOrEmpty(config.WebhookURL))
            {
                throw new ArgumentException($"{nameof(config.WebhookURL)} must be informed");
            }

            builder.AddProvider(new MerchantLoggerProvider(config));

            return builder;
        }

        public static void LogError(this ILogger logger, Exception exception)
        {
            logger.LogError(exception: exception, message: string.Empty, args: Array.Empty<object>());
        }
    }
}
