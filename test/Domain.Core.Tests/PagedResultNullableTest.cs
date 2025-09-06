using Xunit;
using MySvc.Framework.Domain.Core.Paged;

namespace Domain.Core.Tests;

public class PagedResultNullableTest
{
    [Fact]
    public void NullableEquality_BothNull_ShouldReturnTrue()
    {
        // Arrange
        PagedResult<string>? left = null;
        PagedResult<string>? right = null;

        // Act & Assert
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact]
    public void NullableEquality_OneNull_ShouldReturnFalse()
    {
        // Arrange
        PagedResult<string>? left = null;
        var right = new PagedResult<string>(1, 1, 10, 1, new List<string> { "test" });

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
        var left = new PagedResult<string>(2, 1, 10, 1, data);
        var right = new PagedResult<string>(2, 1, 10, 1, data);

        // Act & Assert
        Assert.True(left == right);
        Assert.False(left != right);
    }

    [Fact]
    public void NullableEquality_BothNotNull_DifferentValues_ShouldReturnFalse()
    {
        // Arrange
        var left = new PagedResult<string>(1, 1, 10, 1, new List<string> { "test1" });
        var right = new PagedResult<string>(1, 1, 10, 1, new List<string> { "test2" });

        // Act & Assert
        Assert.False(left == right);
        Assert.True(left != right);
    }
}