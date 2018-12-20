
namespace Tracing.Console
{
    public class ColorConsoleTracerFactory : TracerFactoryBase
    {
        public override ITracer Create(string name)
        {
            return new ColorConsoleTracer(name);
        }
    }
}