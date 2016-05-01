using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace Tracing.IntegrationTests
{
    public class TracerTests
    {
        [Fact]
        public void ShouldInstanciateTracer()
        {
            // Arrange
            Type targetType = typeof(TracerTests); 

            // Act
            ITracer tracer = Tracer.Create(targetType);

            // Assert
            Tracer.Factory.GetType().Should().NotBe(typeof(EmptyTracerFactory));
            tracer.GetType().Should().NotBe(typeof(EmptyTracer));
        }
    }
}
