using System;
using System.Collections.Generic;
using Autofac;
using FluentAssertions;
using Tracing.Autofac;
using Xunit;

namespace Tracing.Tests.Autofac
{
    [Collection("Tracing")]
    public class TracingModuleTests : IDisposable
    {
        public TracingModuleTests()
        {
            Tracer.Initialize();
        }

        public void Dispose()
        {
            Tracer.Initialize();
        }

        [Fact]
        public void ShouldInjectTracerIntoClass__UsingConstructorInjection()
        {
            // Arrange 
            var entries = new List<KeyValuePair<string, TraceEntry>>();
            Tracer.SetFactory(new ActionTracerFactory((s, entry) => { entries.Add(new KeyValuePair<string, TraceEntry>(s, entry)); }));
            var builder = new ContainerBuilder();
            builder.RegisterModule<TracingModule>();
            builder.RegisterType<MyClass>();
            var container = builder.Build();

            // Act
            var myClass = container.Resolve<MyClass>();
            myClass.DoSomething();

            // Assert
            myClass.Should().BeOfType<MyClass>();
            entries.Should().HaveCount(1);
            entries[0].Key.Should().Match("*MyClass[*]");
            entries[0].Value.Message.Should().Contain("Do something...");
        }

        public class MyClass
        {
            private readonly ITracer tracer;

            public MyClass(ITracer tracer)
            {
                this.tracer = tracer;
            }

            public void DoSomething()
            {
                this.tracer.Info("Do something...");
            }
        }
    }
}