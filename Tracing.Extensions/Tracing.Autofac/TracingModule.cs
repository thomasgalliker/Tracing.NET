using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using Module = Autofac.Module;

namespace Tracing.Autofac
{
    /// <summary>
    /// Autofac Module register ITracer dependency which is injected into resolving services.
    /// ITracer is registered as 'Instance Per Dependency' so that a unique tracer instance will be returned from each request for a service.
    /// </summary>
    public class TracingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((c, p) => Tracer.Create(p.TypedAs<Type>()))
                .As(typeof(ITracer))
                .InstancePerDependency();
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
