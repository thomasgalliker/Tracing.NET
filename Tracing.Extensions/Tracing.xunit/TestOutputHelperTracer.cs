using System;
using Xunit.Abstractions;

namespace Tracing.xunit
{
    public class TestOutputHelperTracer : ITracer
    {
        private const string EndOfLine = "[EOL]";
        private readonly ITestOutputHelper testOutputHelper;

        public TestOutputHelperTracer(object target, ITestOutputHelper testOutputHelper)
            : this(target.GetType().Name, testOutputHelper)
        {
        }

        public TestOutputHelperTracer(string targetName, ITestOutputHelper testOutputHelper)
        {
            //Guard.ArgumentNotNullOrEmpty(nameof(targetName), targetName);
            //Guard.ArgumentNotNull(nameof(testOutputHelper), testOutputHelper);

            this.Name = targetName;
            this.testOutputHelper = testOutputHelper;
        }

        public string Name { get; }

        public void Write(Category category, string message, params object[] arguments)
        {
            try
            {
                var messageLine = $"{DateTime.UtcNow} - {category} - {this.Name} - {message} {EndOfLine}";
                this.testOutputHelper.WriteLine(messageLine);
            }
            catch (InvalidOperationException)
            {
                // TestOutputHelperLogger throws an InvalidOperationException
                // if it is no longer associated with a test case.
            }
        }

        public void Write(Category category, Exception exception, string message, params object[] arguments)
        {
            try
            {
                var messageLine = $"{DateTime.UtcNow} - {category} - {this.Name} - {message} - Exception: {exception} {EndOfLine}";
                this.testOutputHelper.WriteLine(messageLine);
            }
            catch (InvalidOperationException)
            {
                // TestOutputHelperLogger throws an InvalidOperationException
                // if it is no longer associated with a test case.
            }
        }
    }
}