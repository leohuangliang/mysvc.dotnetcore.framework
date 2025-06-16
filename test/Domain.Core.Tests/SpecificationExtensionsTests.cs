using System;
using System.Linq.Expressions;
using MySvc.Framework.Domain.Core.Specification;
using Xunit;

namespace MySvc.Framework.Domain.Core.Tests
{
  /// <summary>
  /// SpecificationExtensions扩展方法的单元测试
  /// 验证重载可行性和null安全处理
  /// </summary>
  public class SpecificationExtensionsTests
  {
    // 测试用的简单实体类
    public class TestEntity
    {
      public int Id { get; set; }
      public string Name { get; set; } = string.Empty;
      public int Age { get; set; }
    }

    // 测试用的简单规范实现
    public class TestSpecification : Specification<TestEntity>
    {
      private readonly Expression<Func<TestEntity, bool>> _expression;

      public TestSpecification(Expression<Func<TestEntity, bool>> expression)
      {
        _expression = expression;
      }

      public override Expression<Func<TestEntity, bool>> GetExpression()
      {
        return _expression;
      }
    }

    #region And扩展方法重载测试

    [Fact]
    public void And_WithExpressionParameter_ShouldCompileAndWork()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      Expression<Func<TestEntity, bool>> rightExpression = x => x.Age > 18;

      // Act - 这里验证重载解析是否正确
      var result = leftSpec.And(rightExpression);

      // Assert
      Assert.NotNull(result);
      Assert.NotNull(result.GetExpression());
    }

    [Fact]
    public void And_WithSpecificationParameter_ShouldCompileAndWork()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      ISpecification<TestEntity> rightSpec = new TestSpecification(x => x.Age > 18);

      // Act - 这里验证重载解析是否正确
      var result = leftSpec.And(rightSpec);

      // Assert
      Assert.NotNull(result);
      Assert.NotNull(result.GetExpression());
    }

    [Fact]
    public void And_WithNullLeftAndExpression_ShouldReturnExpressionSpec()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = null;
      Expression<Func<TestEntity, bool>> rightExpression = x => x.Age > 18;

      // Act
      var result = leftSpec.And(rightExpression);

      // Assert
      Assert.NotNull(result);
      Assert.NotNull(result.GetExpression());
    }

    [Fact]
    public void And_WithNullLeftAndSpecification_ShouldReturnRightSpec()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = null;
      ISpecification<TestEntity> rightSpec = new TestSpecification(x => x.Age > 18);

      // Act
      var result = SpecificationExtensions.And(leftSpec, rightSpec);

      // Assert
      Assert.NotNull(result);
      // 验证返回的是右规范（根据扩展方法的实现）
      Assert.Same(rightSpec, result);
    }

    [Fact]
    public void And_WithNullExpression_ShouldThrowArgumentNullException()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      Expression<Func<TestEntity, bool>>? nullExpression = null;

      // Act & Assert
      Assert.Throws<ArgumentNullException>(() => leftSpec.And(nullExpression!));
    }

    [Fact]
    public void And_WithNullSpecification_ShouldThrowArgumentNullException()
    {
      // Arrange
      ISpecification<TestEntity> leftSpec = new TestSpecification(x => x.Id > 0);
      ISpecification<TestEntity>? nullSpec = null;

      // Act & Assert
      Assert.Throws<ArgumentNullException>(() => SpecificationExtensions.And(leftSpec, nullSpec!));
    }

    #endregion

    #region Or扩展方法重载测试

    [Fact]
    public void Or_WithExpressionParameter_ShouldCompileAndWork()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      Expression<Func<TestEntity, bool>> rightExpression = x => x.Age > 18;

      // Act - 验证重载解析
      var result = leftSpec.Or(rightExpression);

      // Assert
      Assert.NotNull(result);
      Assert.NotNull(result.GetExpression());
    }

    [Fact]
    public void Or_WithSpecificationParameter_ShouldCompileAndWork()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      ISpecification<TestEntity> rightSpec = new TestSpecification(x => x.Age > 18);

      // Act - 验证重载解析
      var result = leftSpec.Or(rightSpec);

      // Assert
      Assert.NotNull(result);
      Assert.NotNull(result.GetExpression());
    }

    #endregion

    #region AndIf条件扩展方法测试

    [Fact]
    public void AndIf_WithTrueConditionAndExpression_ShouldApplyAnd()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      Expression<Func<TestEntity, bool>> rightExpression = x => x.Age > 18;

      // Act
      var result = leftSpec.AndIf(true, rightExpression);

      // Assert
      Assert.NotNull(result);
      Assert.NotNull(result.GetExpression());
    }

    [Fact]
    public void AndIf_WithFalseConditionAndExpression_ShouldReturnOriginal()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      Expression<Func<TestEntity, bool>> rightExpression = x => x.Age > 18;

      // Act
      var result = leftSpec.AndIf(false, rightExpression);

      // Assert
      Assert.NotNull(result);
      Assert.Same(leftSpec, result);
    }

    [Fact]
    public void AndIf_WithFalseConditionAndNullLeft_ShouldReturnAnySpec()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = null;
      Expression<Func<TestEntity, bool>> rightExpression = x => x.Age > 18;

      // Act
      var result = leftSpec.AndIf(false, rightExpression);

      // Assert
      Assert.NotNull(result);
      Assert.IsType<AnySpecification<TestEntity>>(result);
    }

    #endregion

    #region OrIf条件扩展方法测试

    [Fact]
    public void OrIf_WithTrueConditionAndExpression_ShouldApplyOr()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      Expression<Func<TestEntity, bool>> rightExpression = x => x.Age > 18;

      // Act
      var result = leftSpec.OrIf(true, rightExpression);

      // Assert
      Assert.NotNull(result);
      Assert.NotNull(result.GetExpression());
    }

    [Fact]
    public void OrIf_WithFalseConditionAndExpression_ShouldReturnOriginal()
    {
      // Arrange
      ISpecification<TestEntity>? leftSpec = new TestSpecification(x => x.Id > 0);
      Expression<Func<TestEntity, bool>> rightExpression = x => x.Age > 18;

      // Act
      var result = leftSpec.OrIf(false, rightExpression);

      // Assert
      Assert.NotNull(result);
      Assert.Same(leftSpec, result);
    }

    #endregion

    #region 重载解析验证测试

    [Fact]
    public void CompilerShouldDistinguishBetweenOverloads()
    {
      // Arrange
      ISpecification<TestEntity>? spec = new TestSpecification(x => x.Id > 0);
      Expression<Func<TestEntity, bool>> expression = x => x.Age > 18;
      ISpecification<TestEntity> anotherSpec = new TestSpecification(x => x.Name != "");

      // Act - 这些调用应该编译成功，并调用不同的重载
      var result1 = spec.And(expression);        // 应该调用表达式重载
      var result2 = spec.And(anotherSpec);       // 应该调用规范对象重载
      var result3 = spec.Or(expression);         // 应该调用表达式重载
      var result4 = spec.Or(anotherSpec);        // 应该调用规范对象重载

      // Assert - 所有结果都应该非空
      Assert.NotNull(result1);
      Assert.NotNull(result2);
      Assert.NotNull(result3);
      Assert.NotNull(result4);
    }

    #endregion

    #region 便捷方法测试

    [Fact]
    public void GetExpressionOrDefault_WithNullSpec_ShouldReturnDefaultExpression()
    {
      // Arrange
      ISpecification<TestEntity>? nullSpec = null;

      // Act
      var result = nullSpec.GetExpressionOrDefault();

      // Assert
      Assert.NotNull(result);
      // 验证默认表达式总是返回true
      var compiledExpression = result.Compile();
      Assert.True(compiledExpression(new TestEntity()));
    }

    [Fact]
    public void OrAny_WithNullSpec_ShouldReturnAnySpecification()
    {
      // Arrange
      ISpecification<TestEntity>? nullSpec = null;

      // Act
      var result = nullSpec.OrAny();

      // Assert
      Assert.NotNull(result);
      Assert.IsType<AnySpecification<TestEntity>>(result);
    }

    [Fact]
    public void OrNone_WithNullSpec_ShouldReturnNoneSpecification()
    {
      // Arrange
      ISpecification<TestEntity>? nullSpec = null;

      // Act
      var result = nullSpec.OrNone();

      // Assert
      Assert.NotNull(result);
      Assert.IsType<NoneSpecification<TestEntity>>(result);
    }

    #endregion
  }
}