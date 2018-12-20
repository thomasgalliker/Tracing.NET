using System;

using Tracing.IoC;

namespace Tracing
{
    /// <summary>
    /// ITracer is the abstraction of a logging framework.
    /// </summary>
    [UseCache(false)]
    public interface ITracer
    {
        void Write(Category category, string message, params object[] arguments);

        void Write(Category category, Exception exception, string message, params object[] arguments);
    }
}