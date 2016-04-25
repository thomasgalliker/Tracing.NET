namespace Tracing
{
    internal interface IDefaultTracerFactoryConfiguration
    {
        ITracerFactory GetDefaultTracerFactory();
    }
}