
using System;
using System.Diagnostics;
using Tracing;
using Tracing.Console;

namespace TracingSample.Netfx
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup tracing at bootstrapping time
            Tracer.SetFactory(new ColorConsoleTracerFactory());

            // Use tracer in production code
            ITracer tracer = Tracer.Create(typeof(Program));
            tracer.Debug("Some debug message");
            tracer.Info("Some info message");
            tracer.Warning("Some warning message");

            try
            {
                // Simulate an exception situation...
                var zero = 0;
                var div = 1 / zero;
            }
            catch (Exception ex)
            {
                var x = ex.ToDetailedString(new ExceptionOptions { IndentSpaces = 8, OmitNullProperties = true });
                tracer.Error(x);

                tracer.Exception(ex);
                tracer.FatalError(new InvalidOperationException("Application crash", ex));
            }

            System.Diagnostics.Trace.Listeners.Clear();
            System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(Console.Out));
            System.Diagnostics.Trace.Listeners.Add(new Tracing.TraceListener(new ColorConsoleTracer(nameof(Trace))));
            System.Diagnostics.Trace.WriteLine($"Trace test...");
            System.Diagnostics.Trace.WriteLine($"Trace test...", "fatal");
            System.Diagnostics.Trace.WriteLine($"Trace test...", "Fatal");
            System.Diagnostics.Trace.WriteLine($"Trace test...", "information");

            //System.Diagnostics.Trace.Listeners.Clear();
            //System.Diagnostics.Trace.Listeners.Add(new ColorConsoleTraceListener(new ColorConsoleTracer(nameof(Trace))));
            var traceSource = new TraceSource("log");
            traceSource.TraceEvent(TraceEventType.Start, 0, "Start message");
            traceSource.TraceInformation("Hello World");
            traceSource.TraceEvent(TraceEventType.Error, 0, "Something failed.");
            traceSource.TraceEvent(TraceEventType.Verbose, 0, "I like ice cream.");
            traceSource.TraceEvent(TraceEventType.Critical, 0, "Something went horribly wrong!");
            traceSource.TraceEvent(TraceEventType.Verbose, 0, "I like cherries.");
            traceSource.TraceEvent(TraceEventType.Warning, 0, "This program will end soon...");
            traceSource.TraceEvent(TraceEventType.Information, 0, "Ending program.");
            traceSource.TraceEvent(TraceEventType.Stop, 0, "Stop message");

            Console.ReadKey();
        }
    }
}
