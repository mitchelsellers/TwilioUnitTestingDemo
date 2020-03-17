using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TwilioDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            var configuration = builder.Build();

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .Configure<TwilioSmsServiceOptions>(configuration.GetSection(nameof(TwilioSmsServiceOptions)))
                .AddSingleton<ISmsService, TwilioSmsService>()
                .AddSingleton<ISampleJob, SampleJob>()
                .BuildServiceProvider();

            //Send our message
            Console.WriteLine("Welcome to the Simple SMS Sender.");
            Console.WriteLine("Destination # (Example +15155551212)");
            var phoneNumber = Console.ReadLine();
            var myServiceInstance = serviceProvider.GetService<ISampleJob>();
            myServiceInstance.SendMessage(phoneNumber, "testing");
            Console.ReadLine();
        }
    }
}
