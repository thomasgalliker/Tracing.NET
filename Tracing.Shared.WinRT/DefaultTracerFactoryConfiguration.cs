using System;
using System.Collections.Generic;
using System.Text;

namespace Tracing.Shared.WinRT
{
    internal class DefaultTracerFactoryConfiguration : IDefaultTracerFactoryConfiguration
    {
        public ITracerFactory GetDefaultTracerFactory()
        {
            return new DebugTracerFactory();
        }
    }
}
