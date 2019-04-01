#if NET45
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Tracing.xunit;
using Xunit;
using Xunit.Abstractions;

namespace Tracing.Tests
{
    [Collection("Tracing")]
    public class TraceListenerTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public TraceListenerTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TraceListenerWriteShouldWriteMessage()
        {
            // Arrange
            var entries = new List<TraceEntry>();

            var testOutputHelperTracer = new TestOutputHelperTracer(this, this.testOutputHelper);
            var actionTracer = new ActionTracer(this, (s, entry) => entries.Add(entry));
            var hybridTracer = new HybridTracer(testOutputHelperTracer, actionTracer);
            var consoleTraceListener = new TraceListener(hybridTracer);

            // Act
            consoleTraceListener.Write($"message");

            // Assert
            entries.Should().HaveCount(1);
            entries[0].Category.Should().Be(Category.Debug);
        }

        [Theory]
        [ClassData(typeof(TraceListenerCategoriesTestData))]
        public void TraceListenerWriteShouldWriteMessageAndCategory(string category)
        {
            // Arrange
            var entries = new List<TraceEntry>();

            var testOutputHelperTracer = new TestOutputHelperTracer(this, this.testOutputHelper);
            var actionTracer = new ActionTracer(this, (s, entry) => entries.Add(entry));
            var hybridTracer = new HybridTracer(testOutputHelperTracer, actionTracer);
            var consoleTraceListener = new TraceListener(hybridTracer);

            // Act
            consoleTraceListener.Write($"message", category);

            // Assert
            entries.Should().HaveCount(1);
        }

        public class TraceListenerCategoriesTestData : TheoryData<string>
        {
            public TraceListenerCategoriesTestData()
            {
                var categories = new HashSet<string>();
                foreach (var value in Enum.GetValues(typeof(TraceEventType)).OfType<TraceEventType>())
                {
                    categories.Add(value.ToString());
                    categories.Add(value.ToString().ToLower());
                }

                foreach (var value in Enum.GetValues(typeof(Category)).OfType<Category>())
                {
                    categories.Add(value.ToString());
                    categories.Add(value.ToString().ToLower());
                }

                foreach (var category in categories)
                {
                    this.Add(category);
                }
            }
        }
    }
}
#endif