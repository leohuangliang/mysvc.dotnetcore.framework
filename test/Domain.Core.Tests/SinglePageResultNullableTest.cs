using Xunit;
using MySvc.Framework.Domain.Core.Paged;

namespace Domain.Core.Tests;

public class SinglePageResultNullableTest
{
    [Fact]
    public void NullableEquality_BothNull_ShouldReturnTrue()
    {
        // Arrange
        SinglePageResult<string>? left = null;
        SinglePageResult<string>? right = null;

        // Act & Assert
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact]
    public void NullableEquality_OneNull_ShouldReturnFalse()
    {
        // Arrange
        SinglePageResult<string>? left = null;
        var right = new SinglePageResult<string>(1, 10, new List<string> { "test" });

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
        var data = new List<string> { "test1", "test2" };
        var left = new SinglePageResult<string>(1, 10, data);
        var right = new SinglePageResult<string>(1, 10, data);

        // Act & Assert
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact]
    public void NullableEquality_BothNotNull_DifferentValues_ShouldReturnFalse()
    {
        // Arrange
        var left = new SinglePageResult<string>(1, 10, new List<string> { "test1" });
        var right = new SinglePageResult<string>(1, 10, new List<string> { "test2" });

        // Act & Assert
        Assert.False(left == right);
        Assert.True(left != right);
    }
}