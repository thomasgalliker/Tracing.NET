
namespace Tracing
{
    public class DebugTracerFactory : TracerFactoryBase
    {
        public override ITracer Create(string name)
        {
            return new DebugTracer(name);
        }
    }
}