namespace Merchant
{
    internal interface IWriter
    {
        void Write(string title, string description, int color, params object[] args);
    }
}
