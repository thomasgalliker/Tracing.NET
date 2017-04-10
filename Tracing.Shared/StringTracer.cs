using System.Diagnostics;

using Tracing.Extensions;

namespace Tracing
{
    public abstract class StringTracer : TracerBase
    {
        private readonly string name;

        protected StringTracer(string name)
        {
            Guard.ArgumentNotNullOrEmpty(nameof(name), name);

            this.name = name;
        }

        protected override void WriteCore(TraceEntry entry)
        {
            var traceString = entry.ToTraceString(this.name);
            this.WriteCore(traceString);
        }

        protected abstract void WriteCore(string traceString);

        public override bool IsCategoryEnabled(Category category)
        {
            return true;
        }
    }
}