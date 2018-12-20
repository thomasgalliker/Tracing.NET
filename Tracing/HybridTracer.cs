using System;

namespace Tracing
{
    public class HybridTracer : ITracer
    {
        private readonly ITracer[] tracers;

        public HybridTracer(params ITracer[] tracers)
        {
            this.tracers = tracers;
        }

        public void Write(Category category, string message, params object[] arguments)
        {
            foreach (var tracer in this.tracers)
            {
                tracer.Write(category, message, arguments);
            }
        }

        public void Write(Category category, Exception exception, string message, params object[] arguments)
        {
            foreach (var tracer in this.tracers)
            {
                tracer.Write(category, exception, message, arguments);
            }
        }
    }
}