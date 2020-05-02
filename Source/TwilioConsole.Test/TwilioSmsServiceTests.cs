using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Twilio.Exceptions;
using Xunit;

namespace TwilioSmsConsole.Test
{
    public class TwilioSmsServiceTests
    {
        private readonly TwilioSettings _options;

        public TwilioSmsServiceTests()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            _options = new TwilioSettings
            {
                AccountSid = configuration["TWILIO_TEST_ACCOUNT_SID"],
                AuthToken = configuration["TWILIO_TEST_AUTH_TOKEN"]
            };
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")] //Empty string
        [InlineData("    ")] //Whitespace string
        public void Send_ShouldThrowArgumentNullException_WithInvalidRecipient(string recipientToTest)
        {
            // Arrange
            var testService = new TwilioSmsService(Options.Create(_options));
            var message = "Valid Message";

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => testService.Send(recipientToTest, message));

            // Assert
            Assert.Equal("recipient", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")] //Empty string
        [InlineData("    ")] //Whitespace string
        public void Send_ShouldThrowArgumentNullException_WithInvalidMessage(string messageToTest)
        {
            var testService = new TwilioSmsService(Options.Create(_options));
            var recipient = "+15555551212";

            var exception = Assert.Throws<ArgumentNullException>(() => testService.Send(recipient, messageToTest));

            Assert.Equal("message", exception.ParamName);
        }

        [Fact]
        public void Send_ShouldThrowTwilioError_WhenInvalidPhoneNumberIsUsed()
        {
            // Arrange
            _options.FromPhoneNumber = "+15005550001"; //Magic number for invalid
            var expectedErrorCode = 21212;
            var recipient = "+15555551212";
            var message = "Testing";
            var testService = new TwilioSmsService(Options.Create(_options));

            // Act
            var exception = Assert.Throws<ApiException>(() => testService.Send(recipient, message));

            // Assert
            Assert.Equal(expectedErrorCode, exception.Code);
        }

        [Fact]
        public void Send_ShouldExecuteWithoutError_WithValidInputs()
        {
            //Arrange
            _options.FromPhoneNumber = "+15005550006";
            var recipient = "+15155551212";
            var message = "Sent from Unit Test";
            var testService = new TwilioSmsService(Options.Create(_options));

            //Act
            testService.Send(recipient, message);

            //Assert
            //TODO: Production implementation should have a return value to be able to validate success.
            Assert.True(true);
        }
    }
}