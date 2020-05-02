using Moq;
using Xunit;

namespace TwilioSmsConsole.Tests
{
    public class SampleJobTests
    {
        private readonly ISampleJob _sampleJob;
        private readonly Mock<ISmsService> _smsServiceMock;

        public SampleJobTests()
        {
            _smsServiceMock = new Mock<ISmsService>();
            _sampleJob = new SampleJob(_smsServiceMock.Object);
        }

        [Fact]
        public void Send_ShouldPassProperValuesToSmsService()
        {
            //Arrange
            var toPhone = "+11111111111";
            var message = "Testing";

            //Act
            _sampleJob.SendMessage(toPhone, message);

            //Assert
            _smsServiceMock.Verify(s => s.Send(toPhone, message), Times.Once);
        }

    }
}