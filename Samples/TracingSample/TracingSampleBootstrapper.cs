using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrossPlatformLibrary.Bootstrapping;

using Tracing;

namespace TracingSample
{
    public class TracingSampleBootstrapper : Bootstrapper
    {
        protected override void OnStartup()
        {
            Tracer.SetFactory(new ActionTracerFactory(
                (s, entry) =>
                    {
                        
                    }));
        }
    }
}
