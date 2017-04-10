using System;

namespace Tracing
{
    public class ConsoleTracer : StringTracer
    {
        public ConsoleTracer(string name) : base(name)
        {
        }

        protected override void WriteCore(string traceMessage)
        {
            Console.WriteLine(traceMessage);
        }
    }
}