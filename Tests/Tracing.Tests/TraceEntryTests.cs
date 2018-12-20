using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Tracing.Tests
{
    public class TraceEntryTests
    {

        [Theory]
        [ClassData(typeof(TraceEntryConstructorTestData))]
        public void ShouldSuccessfullyInitializeTraceEntry(Func<TraceEntry> createTraceEntry, Category expectedCategory, string expectedMessage)
        {
            // Act
            var traceEntry = createTraceEntry();

            // Assert
            traceEntry.Should().NotBeNull();
            traceEntry.Category.Should().Be(expectedCategory);
            traceEntry.Message.Should().Be(expectedMessage);
        }

        public class TraceEntryConstructorTestData : TheoryData<Func<TraceEntry>, Category, string>
        {
            public TraceEntryConstructorTestData()
            {
                foreach (var category in Enum.GetValues(typeof(Category)).OfType<Category>())
                {
                    this.Add(() => new TraceEntry(category, message: null), category, "");
                    this.Add(() => new TraceEntry(category, ""), category, "");
                    this.Add(() => new TraceEntry(category, "message"), category, "message");
                    this.Add(() => new TraceEntry(category, "message with {0}", "arg1"), category, "message with arg1");
                }
            }
        }

        [Fact]
        public void ShouldSetExceptionIfProvided()
        {
            // Arrange
            var exception = new Exception();

            // Act
            var traceEntry = new TraceEntry(Category.Error, exception, "message");

            // Assert
            traceEntry.Exception.Should().Be(exception);
        }
    }
}