using System;
using Microsoft.Extensions.Logging;

namespace Merchant
{
    public class MerchantLogger : ILogger
    {
        private readonly string _categoryName;

        private readonly MerchantLoggerConfiguration _configuration;

        public MerchantLogger(string categoryName, MerchantLoggerConfiguration configuration)
        {
            _categoryName = categoryName;
            _configuration = configuration;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var color = Console.ForegroundColor;

            var prefix = "|> ";

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{prefix}Got some rare things on sale, stranger!");

            Console.ForegroundColor = ConsoleColor.Green;
            var value = $"{Enum.GetName(typeof(LogLevel), logLevel).ToLower()}: {_categoryName}";
            Console.WriteLine(value.PadLeft(value.Length + prefix.Length));

            Console.ForegroundColor = color;
            value = $"{DateTime.UtcNow:O} - {formatter.Invoke(state, exception)}";
            Console.WriteLine(value.PadLeft(value.Length + prefix.Length));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _configuration.LogLevel;
        }
    }
}
