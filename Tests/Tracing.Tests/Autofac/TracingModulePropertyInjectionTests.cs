using System;
using System.Collections.Generic;
using Autofac;
using FluentAssertions;
using Tracing.Autofac;
using Xunit;

namespace Tracing.Tests.Autofac
{
    [Collection("Tracing")]
    public class TracingModulePropertyInjectionTests : IDisposable
    {
        public TracingModulePropertyInjectionTests()
        {
            Tracer.Initialize();
        }

        public void Dispose()
        {
            Tracer.Initialize();
        }

        [Fact]
        public void ShouldInjectTracerIntoClass_UsingPropertyInjection()
        {
            // Arrange 
            var entries = new List<KeyValuePair<string, TraceEntry>>();
            Tracer.SetFactory(new ActionTracerFactory((s, entry) => { entries.Add(new KeyValuePair<string, TraceEntry>(s, entry)); }));
            var builder = new ContainerBuilder();
            builder.RegisterModule<TracingModulePropertyInjection>();
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
            public MyClass()
            {
            }

            public ITracer Tracer { get; set; }

            public void DoSomething()
            {
                this.Tracer.Info("Do something...");
            }
        }
    }
}