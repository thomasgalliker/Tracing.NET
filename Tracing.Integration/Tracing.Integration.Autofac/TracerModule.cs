using System;
using System.Linq;

using Autofac;
using Autofac.Core;

namespace Tracing.Integration.Autofac
{
    public class TracerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) => Tracer.Create(p.TypedAs<Type>())).As(typeof(ITracer));
        }
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing +=
                (sender, args) =>
                {
                    var forType = args.Component.Activator.LimitType;
                    var logParameter = new ResolvedParameter(
                        (p, c) => p.ParameterType == typeof(ITracer),
                        (p, c) => c.Resolve<ITracer>(TypedParameter.From(forType)));
                    args.Parameters = args.Parameters.Union(new[] { logParameter });
                };
        }
    }
}
