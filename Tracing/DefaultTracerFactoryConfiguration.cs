namespace Tracing
{
    internal static class DefaultTracerFactoryConfiguration
    {
        internal static ITracerFactory GetDefaultTracerFactory()
        {
            return new DebugTracerFactory();
        }
    }
}