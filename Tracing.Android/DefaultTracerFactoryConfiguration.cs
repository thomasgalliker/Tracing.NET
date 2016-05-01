namespace Tracing
{
    internal class DefaultTracerFactoryConfiguration
    {
        public ITracerFactory GetDefaultTracerFactory()
        {
            return new AndroidLogTracerFactory();
        }
    }
}