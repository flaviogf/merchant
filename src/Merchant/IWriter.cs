namespace Merchant
{
    internal interface IWriter
    {
        void Write(string title, string description, params object[] args);
    }
}
