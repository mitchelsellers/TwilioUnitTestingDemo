namespace TwilioDemo
{
    /// <summary>
    ///     Defines a service implementation that will deliver SMS messages
    /// </summary>
    public interface ISmsService
    {
        void Send(string recipient, string message);
    }
}