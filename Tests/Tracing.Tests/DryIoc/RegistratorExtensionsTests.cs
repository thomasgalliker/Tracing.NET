using System;
using System.Collections.Generic;
using DryIoc;
using FluentAssertions;
using Tracing.DryIoc;
using Xunit;

namespace Tracing.Tests.DryIoc
{
    [Collection("Tracing")]
    public class RegistratorExtensionsTests : IDisposable
    {
        public RegistratorExtensionsTests()
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

            var container = new Container();
            container.AddTracing();

            container.Register<MyChildClass>();
            container.Register<MyParentClass>();

            // Act
            var myParentClass = container.Resolve<MyParentClass>();
            myParentClass.DoSomething();

            // Assert
            myParentClass.Should().BeOfType<MyParentClass>();

            entries.Should().HaveCount(2);
            entries[0].Key.Should().Match("*MyParentClass[*]");
            entries[0].Value.Message.Should().Contain("Parent does something...");
            entries[1].Key.Should().Match("*MyChildClass[*]");
            entries[1].Value.Message.Should().Contain("Child does something...");
        }

        public class MyParentClass
        {
            private readonly ITracer tracer;
            private readonly MyChildClass myChildClass;

            public MyParentClass(ITracer tracer, MyChildClass myChildClass)
            {
                this.tracer = tracer;
                this.myChildClass = myChildClass;
            }

            public void DoSomething()
            {
                this.tracer.Info("Parent does something...");
                this.myChildClass.DoSomething();
            }
        }
        public class MyChildClass
        {
            private readonly ITracer tracer;

            public MyChildClass(ITracer tracer)
            {
                this.tracer = tracer;
            }

            public void DoSomething()
            {
                this.tracer.Info("Child does something...");
            }
        }
    }
}