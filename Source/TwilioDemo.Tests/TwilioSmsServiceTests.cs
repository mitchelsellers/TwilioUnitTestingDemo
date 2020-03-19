using System;
using Microsoft.Extensions.Options;
using Twilio.Exceptions;
using Xunit;

namespace TwilioDemo.Tests
{
    public class TwilioSmsServiceTests
    {
        /// <summary>
        ///     Options value with  basic information due to additional testing validation
        /// </summary>
        private TwilioSmsServiceOptions _options = new TwilioSmsServiceOptions
            {AccountSid = "<YOURAPIKEY>", AuthToken = "<YOURAUTHTOKEN>" };

        [Theory]
        [InlineData("")] //Empty string
        [InlineData("    ")] //Whitespace string
        public void Send_ShouldThrowArgumentNullException_WithInvalidRecipient(string recipientToTest)
        {
            //Arrange
            var testService = new TwilioSmsService(Options.Create(_options));
            var message = "Valid Message";

            //Act & assert
            var exception = Assert.Throws<ArgumentNullException>(() => testService.Send(recipientToTest, message));
            Assert.Equal("recipient", exception.ParamName);
        }

        [Theory]
        [InlineData("")] //Empty string
        [InlineData("    ")] //Whitespace string
        public void Send_ShouldThrowArgumentNullException_WithInvalidMessage(string messageToTest)
        {
            //Arrange
            var testService = new TwilioSmsService(Options.Create(_options));
            var recipient = "+15555551212";

            //Act & assert
            var exception = Assert.Throws<ArgumentNullException>(() => testService.Send(recipient, messageToTest));
            Assert.Equal("message", exception.ParamName);
        }

        [Fact]
        public void Send_ShouldThrowTwilioError_WhenInvalidPhoneNumberIsUsed()
        {
            //Arrange
            _options.FromPhoneNumber = "+15005550001"; //Magic number for invalid
            var expectedErrorCode = 21212;
            var recipent = "+15555551212";
            var message = "Testing";
            var testService = new TwilioSmsService(Options.Create(_options));

            //Act/Assert
            var exception = Assert.Throws<ApiException>(() => testService.Send(recipent, message));
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