using System;
using Microsoft.Extensions.Logging;

namespace Merchant
{
    internal class MerchantLogger : ILogger
    {
        private readonly string _name;

        private readonly IWriter _writer;

        public MerchantLogger(string name, IWriter writer)
        {
            _name = name;
            _writer = writer;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var title = $"Got some rare things on sale, stranger 💥";

            var subtitle = $"{Enum.GetName(typeof(LogLevel), logLevel).ToLower()}: {_name}";

            var description = formatter.Invoke(state, exception);

            _writer.Write(title, subtitle, description);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Error;
        }
    }
}
