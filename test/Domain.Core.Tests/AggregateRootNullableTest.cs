using MySvc.Framework.Domain.Core.Impl;
using Xunit;

namespace Domain.Core.Tests
{
    /// <summary>
    /// 测试AggregateRoot的可空类型支持
    /// </summary>
    public class AggregateRootNullableTest
    {
        /// <summary>
        /// 测试用的聚合根
        /// </summary>
        public class TestAggregateRoot : AggregateRoot
        {
            public string? Name { get; set; }
            
            public TestAggregateRoot() : base()
            {
            }
            
            public TestAggregateRoot(string id) : base(id)
            {
            }
        }

        [Fact]
        public void NullableEquality_BothNull_ShouldReturnTrue()
        {
            // Arrange
            TestAggregateRoot? left = null;
            TestAggregateRoot? right = null;

            // Act & Assert
            Assert.True(left == right);
            Assert.False(left != right);
        }

        [Fact]
        public void NullableEquality_OneNull_ShouldReturnFalse()
        {
            // Arrange
            TestAggregateRoot? left = null;
            TestAggregateRoot? right = new TestAggregateRoot("test-id") { Name = "Test" };

            // Act & Assert
            Assert.False(left == right);
            Assert.True(left != right);
            Assert.False(right == left);
            Assert.True(right != left);
        }

        [Fact]
        public void NullableEquality_BothNotNull_SameId_ShouldReturnTrue()
        {
            // Arrange
            var id = "test-id";
            TestAggregateRoot? left = new TestAggregateRoot(id) { Name = "Test1" };
            TestAggregateRoot? right = new TestAggregateRoot(id) { Name = "Test2" };

            // Act & Assert
            Assert.True(left == right);
            Assert.False(left != right);
        }

        [Fact]
        public void NullableEquality_BothNotNull_DifferentId_ShouldReturnFalse()
        {
            // Arrange
            TestAggregateRoot? left = new TestAggregateRoot("id1") { Name = "Test" };
            TestAggregateRoot? right = new TestAggregateRoot("id2") { Name = "Test" };

            // Act & Assert
            Assert.False(left == right);
            Assert.True(left != right);
        }
    }
}