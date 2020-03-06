using System;

namespace TwilioDemo
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
            //TODO: handle validation/errors/etc
            _smsService.Send(recipient, text);
            Console.WriteLine("SMS Sent");
        }
    }
}