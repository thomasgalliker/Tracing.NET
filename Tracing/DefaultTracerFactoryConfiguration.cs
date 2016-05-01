using System;

namespace Tracing
{
    public class DefaultTracerFactoryConfiguration
    {
        public ITracerFactory GetDefaultTracerFactory()
        {
            throw new InvalidOperationException();
        }
    }
}