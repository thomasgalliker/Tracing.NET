using System.Linq;
using System.Reflection;
using Autofac.Core;

namespace Tracing.Autofac
{
    /// <summary>
    /// Autofac Module register ITracer dependency which is injected into resolving services.
    /// ITracer is registered as 'Instance Per Dependency' so that a unique tracer instance will be returned from each request for a service.
    /// </summary>
    public class TracingModulePropertyInjection : TracingModule
    {
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);

            // Handle properties.
            registration.Activated += (sender, e) => InjectLoggerProperties(e.Instance);
        }

        private static void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType.GetRuntimeProperties()
                .Where(p => p.PropertyType == typeof(ITracer) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, Tracer.Create(instanceType), null);
            }
        }
    }
}
