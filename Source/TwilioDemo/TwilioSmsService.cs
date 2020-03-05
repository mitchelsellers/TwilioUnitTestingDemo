using System;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TwilioDemo
{
    public class TwilioSmsService : ISmsService
    {
        private readonly TwilioSmsServiceOptions _options;

        public TwilioSmsService(IOptions<TwilioSmsServiceOptions> options)
        {
            _options = options.Value;
        }

        public void Send(string recipient, string message)
        {
            //Validate Inputs
            if (string.IsNullOrEmpty(recipient))
                throw new ArgumentNullException(nameof(recipient));
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            //Must be less than or equal 1600 characters
            if (message.Length >= 1600)
                throw new ArgumentOutOfRangeException(nameof(message), "Message cannot be longer than 1600 chars");

            //Send the message
            TwilioClient.Init(_options.AccountSid, _options.AuthToken);
            var sentMessage = MessageResource.Create(body: message, from: new PhoneNumber(_options.FromPhoneNumber),
                to: new PhoneNumber(recipient));
        }
    }
}