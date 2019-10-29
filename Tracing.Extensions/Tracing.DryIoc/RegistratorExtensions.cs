using System;
using DryIoc;

namespace Tracing.DryIoc
{
    /// <summary>
    /// DryIoc Module registers ITracer dependency which is injected into resolving services.
    /// ITracer is registered as 'Transient' service so that a unique tracer instance will be returned from each request for a service.
    /// </summary>
    public static class RegistratorExtensions
    {
        public static void AddTracing(this IRegistrator container)
        {
            container.Register<ITracer>(
                reuse: Reuse.Transient,
                made: Made.Of(() => Tracer.Create(Arg.Index<Type>(0)), request => request.Parent.ImplementationType),
            setup: Setup.With(condition: r => r.Parent.ImplementationType != null));
        }
    }
}
