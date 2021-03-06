﻿using System;
using FluentAssertions;
using Moq;

using Xunit;

namespace Tracing.Tests
{
    [Collection("Tracing")]
    public class TracerTests : IDisposable
    {
        public TracerTests()
        {
            Tracer.Initialize();
        }

        public void Dispose()
        {
            Tracer.Initialize();
        }

        [Fact]
        public void ShouldReturnDefaultFactoryIfNoFactoryIsSet()
        {
            // Act
            var factory = Tracer.Factory;

            // Assert
            factory.Should().BeOfType<DebugTracerFactory>();
        }

        [Fact]
        public void ShouldSetFactory()
        {
            // Arrange
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();

            // Act
            Tracer.SetFactory(factoryMock.Object);

            // Assert
            Assert.Same(factoryMock.Object, Tracer.Factory);
            mocks.VerifyAll();
        }

        [Fact]
        public void ShouldReturnDefaultFactoryIfFactoryIsSetNull()
        {
            // Arrange
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();

            Tracer.SetFactory(factoryMock.Object);
            Assert.Same(factoryMock.Object, Tracer.Factory);

            // Act
            Tracer.SetFactory(null);

            // Assert
            Assert.IsType<EmptyTracerFactory>(Tracer.Factory);
            mocks.VerifyAll();
        }

        [Fact]
        public void ShouldOverrideDefaultFactory()
        {
            // Arrange
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();
            Tracer.SetFactory(null);

            // Act
            Tracer.SetDefaultFactory(factoryMock.Object);

            // Assert
            Assert.Same(factoryMock.Object, Tracer.Factory);
            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithNameCallsCreateOnFactory()
        {
            // Arrange
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();
            var tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create("Name")).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);

            // Act
            Tracer.Create("Name");

            // Assert
            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithNameReturnsTracerReturnedByFactory()
        {
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();
            var tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create("Name")).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            ITracer tracer = Tracer.Create("Name");
            Assert.Same(tracerMock.Object, tracer);

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithTypeCallsCreateOnFactory()
        {
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();
            var tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create(typeof(Type))).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            Tracer.Create(typeof(Type));

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithTypeReturnsTracerReturnedByFactory()
        {
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();
            var tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create(typeof(Type))).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            ITracer tracer = Tracer.Create(typeof(Type));
            Assert.Same(tracerMock.Object, tracer);

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithGenericTypeCallsCreateOnFactory()
        {
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();
            var tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create<Type>()).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            Tracer.Create<Type>();

            mocks.VerifyAll();
        }

        [Fact]
        public void CreateWithGenericTypeReturnsTracerReturnedByFactory()
        {
            var mocks = new MockRepository(MockBehavior.Strict);
            var factoryMock = mocks.Create<ITracerFactory>();
            var tracerMock = mocks.Create<ITracer>();

            factoryMock.Setup(factory => factory.Create<Type>()).Returns(tracerMock.Object);

            Tracer.SetFactory(factoryMock.Object);
            ITracer tracer = Tracer.Create<Type>();
            Assert.Same(tracerMock.Object, tracer);

            mocks.VerifyAll();
        }

        [Fact]
        public void TestCodeForReadme()
        {
            // Create an ITracer with 'this' as target name
            ITracer tracer = Tracer.Create(this);

            // Create an ITracer with MyClass as generic target name
            ITracer tracerGeneric = Tracer.Create<MyClass>();

            // Create an ITracer with "MyClass" string as target name
            ITracer tracerStringName = Tracer.Create("MyClass");
        }

        public class MyClass
        {
        }
    }
}