using System;
using Microsoft.Extensions.Logging;

namespace Merchant
{
    internal class MerchantLogger : ILogger
    {
        private static readonly Random _random = new Random();

        private static readonly string[] _quotes = new string[]
        {
            "What're ya buyin?",
            "What're ya sellin'?",
            "Not enough cash, stranger!",
            "Got some rare things on sale, stranger!",
            "A wise choice, mate! Its ammo will penetrate just about anything!",
            "Ah, the choice of an avid gun collector! It's a nice gun, mate!",
            "Stranger, stranger, stranger... now that's a weapon!",
            "Not only will you need cash, but you'll need GUTS to buy that weapon!",
            "I see you have an eye for things. Gun's not just about shoot'n, i'ts about reload'n! You'll know what I'm talkin' about!",
            "Is that all, stranger?",
            "Heh heh heh... Thank you!",
            "Ah! I'll buy it at a high price!"
        };

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

            var title = NextQuote();

            var subtitle = $"{Enum.GetName(typeof(LogLevel), logLevel).ToLower()}: {_name}";

            var description = formatter.Invoke(state, exception);

            _writer.Write(title, subtitle, description);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Error;
        }

        private string NextQuote()
        {
            var index = _random.Next(0, _quotes.Length);

            return _quotes[index];
        }
    }
}
