using System;
using FluentAssertions;
using Tracing.Internal;
using Xunit;

namespace Tracing.Tests.Internal
{
    public class MessageFormatterTests
    {
        [Theory]
        [ClassData(typeof(MessageFormatTestData))]
        public void ShouldThrowFormatExceptionIfFormatIsInvalid(string message, object[] arguments)
        {
            // Act
            Action action = () => message.FormatArguments(arguments);

            // Assert
            action.Should().Throw<FormatException>();
        }

        public class MessageFormatTestData : TheoryData<string, object[]>
        {
            public MessageFormatTestData()
            {
                this.Add("message{-1}", new object[] { "arg0" });
                this.Add("message{0}{1}", new object[] { "arg1" });
                this.Add("message{1}", new object[] { "arg1" });
                this.Add("message{0}{1}", new object[] { "arg1" });
            }
        }
    }
}