﻿using System;

namespace Tracing.Extensions
{
    internal static class TraceEntryExtensions
    {
        internal static string ToTraceString(this TraceEntry traceEntry, string loggerName)
        {
            return traceEntry.Exception == null
                       ? $"{DateTime.UtcNow:u} - {traceEntry.Category} - {loggerName} - {traceEntry.Message} [EOL]"
                       : $"{DateTime.UtcNow:u} - {traceEntry.Category} - {loggerName} - {traceEntry.Message} - Exception: {traceEntry.Exception} [EOL]";
        }
    }
}
