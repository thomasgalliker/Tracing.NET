using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tracing;

namespace TracingSample.Netfx
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrapper = new TracingSampleBootstrapper();
            bootstrapper.Startup();

            ITracer tracer = Tracer.Create(typeof(Program));
            tracer.Debug("Some debug message");
            tracer.Info("Some info message");
        }
    }
}
