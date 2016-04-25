namespace Tracing
{
    public class AndroidLogTracerFactory : TracerFactoryBase
    {
        public override ITracer Create(string name)
        {
            return new AndroidLogTracer(name);
        }
    }
}