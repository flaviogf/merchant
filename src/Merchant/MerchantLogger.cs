using System;
using System.Collections.Generic;
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

        private static readonly IDictionary<LogLevel, int> _colors = new Dictionary<LogLevel, int>
        {
            [LogLevel.Warning] = 15844367,
            [LogLevel.Error] = 14500161,
            [LogLevel.Critical] = 16426522
        };

        private readonly string _target;

        private readonly string _application;

        private readonly string _version;

        private readonly string _environment;

        private readonly IWriter _writer;

        public MerchantLogger(string target, string application, string version, string environment, IWriter writer)
        {
            _target = target;
            _application = application;
            _version = version;
            _environment = environment;
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

            var fields = new object[]
            {
                new
                {
                    name = "Application:",
                    value = GetValueOrNothing(_application)
                },
                new
                {
                    name = "Version:",
                    value = GetValueOrNothing(_version)
                },
                new
                {
                    name = "Environment:",
                    value = GetValueOrNothing(_environment)
                },
                new
                {
                    name = "Friendly Message:",
                    value = GetValueOrNothing(formatter?.Invoke(state, exception))
                },
                new
                {
                    name = "Message:",
                    value = GetValueOrNothing(exception?.Message)
                },
                new
                {
                    name = "HelpLink:",
                    value = GetValueOrNothing(exception?.HelpLink)
                },
                new
                {
                    name = "Source:",
                    value = GetValueOrNothing(exception?.Source)
                },
                new
                {
                    name = "StackTrace:",
                    value = GetValueOrNothing(exception?.StackTrace)
                },
                new
                {
                    name = "TargetSite:",
                    value = GetValueOrNothing(exception?.TargetSite)
                }
            };

            _writer.Write(title: NextQuote(), description: _target, color: _colors[logLevel], args: fields);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Warning;
        }

        private string NextQuote()
        {
            var index = _random.Next(0, _quotes.Length);

            return _quotes[index];
        }

        private string GetValueOrNothing(object value, int limit = 1024)
        {
            var result = value?.ToString();

            if (string.IsNullOrWhiteSpace(result))
            {
                return "Nothing";
            }

            if (result.Length >= limit)
            {
                return $"{result.Substring(0, limit - 3)}...";
            }

            return result;
        }
    }
}
