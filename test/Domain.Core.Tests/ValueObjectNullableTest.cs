using MySvc.Framework.Domain.Core.Impl;
using Xunit;

namespace Domain.Core.Tests
{
    /// <summary>
    /// 测试ValueObject的可空类型支持
    /// </summary>
    public class ValueObjectNullableTest
    {
        /// <summary>
        /// 测试用的值对象
        /// </summary>
        public class TestValueObject : ValueObject<TestValueObject>
        {
            public string? Name { get; set; }
            public int Age { get; set; }
        }

        [Fact]
        public void NullableEquality_BothNull_ShouldReturnTrue()
        {
            // Arrange
            TestValueObject? left = null;
            TestValueObject? right = null;

            // Act & Assert
            Assert.True(left == right);
            Assert.False(left != right);
        }

        [Fact]
        public void NullableEquality_OneNull_ShouldReturnFalse()
        {
            // Arrange
            TestValueObject? left = null;
            TestValueObject? right = new TestValueObject { Name = "Test", Age = 25 };

            // Act & Assert
            Assert.False(left == right);
            Assert.True(left != right);
            Assert.False(right == left);
            Assert.True(right != left);
        }

        [Fact]
        public void NullableEquality_BothNotNull_SameValues_ShouldReturnTrue()
        {
            // Arrange
            TestValueObject? left = new TestValueObject { Name = "Test", Age = 25 };
            TestValueObject? right = new TestValueObject { Name = "Test", Age = 25 };

            // Act & Assert
            Assert.True(left == right);
            Assert.False(left != right);
        }

        [Fact]
        public void NullableEquality_BothNotNull_DifferentValues_ShouldReturnFalse()
        {
            // Arrange
            TestValueObject? left = new TestValueObject { Name = "Test1", Age = 25 };
            TestValueObject? right = new TestValueObject { Name = "Test2", Age = 25 };

            // Act & Assert
            Assert.False(left == right);
            Assert.True(left != right);
        }
    }
}