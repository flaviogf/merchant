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

        public ILogger CreateLogger(string categoryName)
        {
            return new MerchantLogger(categoryName, _configuration);
        }

        public void Dispose()
        {
        }
    }
}
