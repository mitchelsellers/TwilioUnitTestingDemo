using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TwilioSmsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .Configure<TwilioSettings>(settings =>
                {
                    settings.AccountSid = configuration["TWILIO_ACCOUNT_SID"];
                    settings.AuthToken = configuration["TWILIO_AUTH_TOKEN"];
                    settings.FromPhoneNumber = configuration["TWILIO_SMS_PHONE_NUMBER"];
                })
                .AddSingleton<ISmsService, TwilioSmsService>()
                .AddSingleton<ISampleJob, SampleJob>()
                .BuildServiceProvider();

            Console.WriteLine("Welcome to the Simple SMS Sender.");
            Console.WriteLine("Enter the recipient's phone number in E.164 format (e.g., +2125551212): ");
            var phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter a message: ");
            var message = Console.ReadLine();
            var myServiceInstance = serviceProvider.GetService<ISampleJob>();
            myServiceInstance.SendMessage(phoneNumber, message);
            Console.WriteLine();
        }
    }
}