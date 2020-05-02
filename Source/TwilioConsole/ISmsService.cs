namespace TwilioSmsConsole
{
    public interface ISmsService
    {
        void Send(string recipient, string message);
    }
}