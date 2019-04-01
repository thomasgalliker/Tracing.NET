#if NET45
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Tracing.Internal;

namespace Tracing
{
    public class TraceListener : System.Diagnostics.TraceListener
    {
        private readonly ITracer tracer;

        public TraceListener(string name) : this(Tracer.Create(name))
        {
        }

        public TraceListener(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public override void Write(string message)
        {
            this.tracer.Write(Category.Debug, message);
        }

        public override void WriteLine(string message)
        {
            this.tracer.Write(Category.Debug, message);
        }

        public override void WriteLine(string message, string category)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            var categoryEnum = ConvertTraceEventTypeToCategory(category);
            this.tracer.Write(categoryEnum, message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                return;
            }

            this.TraceEvent(eventCache, source, eventType, id, "{0}", message);
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message, params object[] args)
        {
            if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message, args, null, null))
            {
                return;
            }

            var category = ConvertTraceEventTypeToCategory(eventType);
            var formattedMessage = message.FormatArguments(args);
            this.tracer.Write(category, formattedMessage);
        }

        private static Category ConvertTraceEventTypeToCategory(string category)
        {
            TraceEventType traceEventType;
            try
            {
                traceEventType = (TraceEventType)Enum.Parse(typeof(TraceEventType), category, ignoreCase: true);

            }
            catch (ArgumentException)
            {
                try
                {
                    return (Category)Enum.Parse(typeof(Category), category, ignoreCase: true);
                }
                catch (ArgumentException)
                {
                    return Category.Debug;
                }
            }

            return ConvertTraceEventTypeToCategory(traceEventType);
        }

        private static Category ConvertTraceEventTypeToCategory(TraceEventType traceEventType)
        {
            Category category;

            switch (traceEventType)
            {
                case TraceEventType.Verbose:
                case TraceEventType.Start:
                case TraceEventType.Stop:
                case TraceEventType.Suspend:
                case TraceEventType.Resume:
                case TraceEventType.Transfer:
                    category = Category.Debug;
                    break;
                case TraceEventType.Information:
                    category = Category.Information;
                    break;

                case TraceEventType.Warning:
                    category = Category.Warning;
                    break;

                case TraceEventType.Error:
                    category = Category.Error;
                    break;

                case TraceEventType.Critical:
                    category = Category.Fatal;
                    break;

                default:
                    throw new InvalidOperationException($"ConvertTraceEventTypeToCategory could not map TraceEventType {traceEventType} to Category enum.");
            }

            return category;
        }
    }
}
#endif