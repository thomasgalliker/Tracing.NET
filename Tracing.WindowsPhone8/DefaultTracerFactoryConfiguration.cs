namespace Tracing
{
    public class DefaultTracerFactoryConfiguration
    {
        public ITracerFactory GetDefaultTracerFactory()
        {
            return new ConsoleTracerFactory();
        }
    }
}