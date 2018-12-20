namespace Tracing.Console
{
    public class ConsoleTracer : StringTracer
    {
        public ConsoleTracer(string name) : base(name)
        {
        }

        protected override void WriteCore(string traceMessage)
        {
            System.Console.WriteLine(traceMessage);
        }
    }
}