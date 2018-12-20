using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Tracing.Tests
{
    [Collection("Tracing")]
    public class EmptyTracerTests
    {
        [Theory]
        [ClassData(typeof(AllCategoriesTestData))]
        public void WriteThrowsIfMessageIsNull(Category category)
        {
            // Arrange
            var tracer = new EmptyTracer();

            // Act
            Action action = () => tracer.Write(category, message: null);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        public class AllCategoriesTestData : TheoryData<Category>
        {
            public AllCategoriesTestData()
            {
                foreach (var value in Enum.GetValues(typeof(Category)).OfType<Category>())
                {
                    this.Add(value);
                }
            }
        }

        [Theory]
        [ClassData(typeof(AllCategoriesTestData))]
        public void WriteNotThrowsIfExceptionIsNull(Category category)
        {
            // Arrange
            var tracer = new EmptyTracer();

            // Act
            Action action = () => tracer.Write(category, exception: null, message: "message");

            // Assert
            action.Should().NotThrow();
        }

        [Theory]
        [ClassData(typeof(AllCategoriesTestData))]
        public void IsCategoryEnabledAlwaysReturnsFalse(Category category)
        {
            // Arrange
            var tracer = new EmptyTracer();

            // Act
            var isCategoryEnabled = tracer.IsCategoryEnabled(category);

            // Assert
            isCategoryEnabled.Should().BeFalse();
        }
    }
}