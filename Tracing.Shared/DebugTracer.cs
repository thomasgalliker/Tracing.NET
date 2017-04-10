#define DEBUG // Necessary to allow the compiler to emit Debug.WriteLine to IL

using System.Diagnostics;

using Tracing.Extensions;

namespace Tracing
{
    /// <summary>
    ///     DebugTracer is a tracer instance which writes trace entries to the
    ///     trace listeners in the <see cref="P:System.Diagnostics.Debug.Listeners" /> collection.
    ///     In Visual Studio > Tools > Debugging > General, the setting for "Redirect all Output Window text to the Immediate
    ///     Window" needs to be checked,
    ///     in order to see Debug.Writeline messages.
    /// </summary>
    public class DebugTracer : StringTracer
    {
        public DebugTracer(string name) : base(name)
        {
        }

        protected override void WriteCore(string traceMessage)
        {
            Debug.WriteLine(traceMessage);
        }
    }
}