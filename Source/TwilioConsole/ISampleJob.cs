namespace TwilioSmsConsole
{
    public interface ISampleJob
    {
        void SendMessage(string recipient, string text);
    }
}