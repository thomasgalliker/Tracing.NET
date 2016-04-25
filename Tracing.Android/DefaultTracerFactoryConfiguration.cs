namespace Tracing
{
    internal class DefaultTracerFactoryConfiguration : IDefaultTracerFactoryConfiguration
    {
        public ITracerFactory GetDefaultTracerFactory()
        {
            return new AndroidLogTracerFactory();
        }
    }
}