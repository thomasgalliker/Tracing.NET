namespace Tracing.Integration.Autofac.Tests.Stubs
{
    public class MyTestClass
    {
        public MyTestClass(ITracer tracer)
        {
            this.Tracer = tracer;
        }

        public ITracer Tracer { get; private set; }
    }
}
