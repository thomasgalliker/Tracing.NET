using System;
using System.Threading;
using FluentAssertions;
using FluentAssertions.Common;
using Xunit;

namespace Tracing.Tests
{
    [Collection("Tracing")]
    public class TraceEntryTests
    {

        [Theory]
        [ClassData(typeof(FailedInitializations))]
        public void ShouldSuccessfullyInitializeTraceEntry(Func<TraceEntry> createTraceEntry)
        {
            //Arrange 

            // Act
            var traceEntry = createTraceEntry();

            // Assert
            traceEntry.Should().NotBeNull();
        }

        public class SuccessInitializations : TheoryData<Func<TraceEntry>>
        {
            public SuccessInitializations()
            {
                this.Add(() => new TraceEntry(Category.Debug, null));
                this.Add(() => new TraceEntry(Category.Debug, ""));
                this.Add(() => new TraceEntry(Category.Information, null));
                this.Add(() => new TraceEntry(Category.Information, ""));
                this.Add(() => new TraceEntry(Category.Warning, null));
                this.Add(() => new TraceEntry(Category.Warning, ""));
                this.Add(() => new TraceEntry(Category.Error, null));
                this.Add(() => new TraceEntry(Category.Error, ""));
                this.Add(() => new TraceEntry(Category.Fatal, null));
                this.Add(() => new TraceEntry(Category.Fatal, ""));
            }
        }

        [Theory]
        [ClassData(typeof(FailedInitializations))]
        public void ShouldFailToInitializeTraceEntry(Action createTraceEntry, Type expectedException)
        {
            // Act // Assert
            Assert.Throws(expectedException, createTraceEntry);
        }

        public class FailedInitializations : TheoryData<Action, Type>
        {
            public FailedInitializations()
            {
                this.Add(() => new TraceEntry(Category.Debug, "message{-1}", "arg0"), typeof(FormatException));
                this.Add(() => new TraceEntry(Category.Debug, "message{0}{1}", "arg1"), typeof(FormatException));
                this.Add(() => new TraceEntry(Category.Debug, "message{1}", "arg1"), typeof(FormatException));
                this.Add(() => new TraceEntry(Category.Debug, "message{0}{1}", "arg1"), typeof(FormatException));
            }
        }

        [Fact]
        public void PropertiesAreInitializedCorrectly()
        {
            Exception exception = new Exception();

            TraceEntry entry = new TraceEntry(Category.Debug, "message");
            Assert.Equal(Category.Debug, entry.Category);
            Assert.Equal("message", entry.Message);

            entry = new TraceEntry(Category.Debug, "{0}", "arg1");
            Assert.Equal(Category.Debug, entry.Category);
            Assert.Equal("arg1", entry.Message);

            entry = new TraceEntry(Category.Debug, exception, "message");
            Assert.Equal(Category.Debug, entry.Category);
            Assert.Equal("message", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Debug, exception, "{0}", "arg1");
            Assert.Equal(Category.Debug, entry.Category);
            Assert.Equal("arg1", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Information, "message");
            Assert.Equal(Category.Information, entry.Category);
            Assert.Equal("message", entry.Message);

            entry = new TraceEntry(Category.Information, "{0}", "arg1");
            Assert.Equal(Category.Information, entry.Category);
            Assert.Equal("arg1", entry.Message);

            entry = new TraceEntry(Category.Information, exception, "message");
            Assert.Equal(Category.Information, entry.Category);
            Assert.Equal("message", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Information, exception, "{0}", "arg1");
            Assert.Equal(Category.Information, entry.Category);
            Assert.Equal("arg1", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Warning, "message");
            Assert.Equal(Category.Warning, entry.Category);
            Assert.Equal("message", entry.Message);

            entry = new TraceEntry(Category.Warning, "{0}", "arg1");
            Assert.Equal(Category.Warning, entry.Category);
            Assert.Equal("arg1", entry.Message);

            entry = new TraceEntry(Category.Warning, exception, "message");
            Assert.Equal(Category.Warning, entry.Category);
            Assert.Equal("message", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Warning, exception, "{0}", "arg1");
            Assert.Equal(Category.Warning, entry.Category);
            Assert.Equal("arg1", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Error, "message");
            Assert.Equal(Category.Error, entry.Category);
            Assert.Equal("message", entry.Message);

            entry = new TraceEntry(Category.Error, "{0}", "arg1");
            Assert.Equal(Category.Error, entry.Category);
            Assert.Equal("arg1", entry.Message);

            entry = new TraceEntry(Category.Error, exception, "message");
            Assert.Equal(Category.Error, entry.Category);
            Assert.Equal("message", entry.Message);
            Assert.Same(exception, entry.Exception);

            entry = new TraceEntry(Category.Error, exception, "{0}", "arg1");
            Assert.Equal(Category.Error, entry.Category);
            Assert.Equal("arg1", entry.Message);
            Assert.Same(exception, entry.Exception);
        }
    }
}