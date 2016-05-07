
using System.Reflection;

using Autofac;

using FluentAssertions;

using Tracing.Integration.Autofac.Tests.Stubs;

using Xunit;

namespace Tracing.Integration.Autofac.Tests
{
    public class TracerModuleTests
    {
        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(Assembly.Load("Tracing.Integration.Autofac"));
            builder.RegisterType<MyTestClass>();
            return builder.Build();
        }

        [Fact]
        public void ShouldInjectConsoleTracer()
        {
            // Arrange
            var container = BuildContainer();

            // Act
            MyTestClass myTestClass = container.Resolve<MyTestClass>();

            // Assert
            myTestClass.Tracer.Should().NotBeNull();
            myTestClass.Tracer.Should().BeOfType<ConsoleTracer>();
        }
    }
}
