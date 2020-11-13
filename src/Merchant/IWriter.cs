using System;

namespace Merchant
{
    internal interface IWriter : IDisposable
    {
        void Write(string title, string subtitle, string description);
    }
}
