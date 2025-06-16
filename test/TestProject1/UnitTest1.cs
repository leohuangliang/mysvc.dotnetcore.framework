namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var expected = 4;
        
        // Act
        var actual = 2 + 2;
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Test2()
    {
        // Test string operations
        var text = "Hello World";
        Assert.Contains("World", text);
        Assert.StartsWith("Hello", text);
    }
    
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 3, 5)]
    [InlineData(-1, 1, 0)]
    public void AddTest(int a, int b, int expected)
    {
        // Act
        var result = a + b;
        
        // Assert
        Assert.Equal(expected, result);
    }
}
