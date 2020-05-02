using System;

namespace TwilioSmsConsole
{
    public class SampleJob : ISampleJob
    {
        private readonly ISmsService _smsService;

        public SampleJob(ISmsService smsService)
        {
            _smsService = smsService;
        }

        public void SendMessage(string recipient, string text)
        {
            Console.WriteLine("Sending SMS");
            _smsService.Send(recipient, text);
            Console.WriteLine("SMS Sent");
        }
    }
}