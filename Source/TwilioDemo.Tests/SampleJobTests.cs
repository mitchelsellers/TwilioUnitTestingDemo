using Moq;
using Xunit;

namespace TwilioDemo.Tests
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
            var toAddress = "+11111111111";
            var message = "Testing";
            
            //Act
            _sampleJob.SendMessage(toAddress, message);

            //Assert
            _smsServiceMock.Verify(s => s.Send(toAddress, message), Times.Once);
        }
    }
}