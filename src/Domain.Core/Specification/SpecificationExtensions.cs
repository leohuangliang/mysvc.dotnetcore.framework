#nullable enable
using System;
using System.Linq.Expressions;

namespace MySvc.Framework.Domain.Core.Specification
{
  /// <summary>
  /// 提供规范模式的扩展方法，支持null安全操作
  /// </summary>
  public static class SpecificationExtensions
  {
    #region And扩展方法

    /// <summary>
    /// And操作，支持表达式参数
    /// 如果左规范为null，创建新的表达式规范；否则返回And组合
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="left">左规范（可为null）</param>
    /// <param name="expression">右侧表达式</param>
    /// <returns>组合后的规范（非null）</returns>
    public static ISpecification<T> And<T>(this ISpecification<T>? left, Expression<Func<T, bool>> expression)
    {
      if (expression == null)
        throw new ArgumentNullException(nameof(expression));

      var rightSpecification = Specification<T>.Eval(expression);

      if (left == null)
        return rightSpecification;

      return new AndSpecification<T>(left, rightSpecification);
    }

    /// <summary>
    /// And操作，支持规范对象参数
    /// 如果左规范为null，返回右规范；否则返回And组合
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="left">左规范（可为null）</param>
    /// <param name="right">右规范（不可为null）</param>
    /// <returns>组合后的规范（非null）</returns>
    public static ISpecification<T> And<T>(this ISpecification<T>? left, ISpecification<T> right)
    {
      if (right == null)
        throw new ArgumentNullException(nameof(right));

      if (left == null)
        return right;

      return new AndSpecification<T>(left, right);
    }

    /// <summary>
    /// 条件And操作，支持表达式参数
    /// 只有当条件为true时才执行And操作
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="left">左规范（可为null）</param>
    /// <param name="condition">条件</param>
    /// <param name="expression">当条件为true时要And的表达式</param>
    /// <returns>组合后的规范（非null）</returns>
    public static ISpecification<T> AndIf<T>(this ISpecification<T>? left, bool condition, Expression<Func<T, bool>> expression)
    {
      if (!condition)
        return left ?? new AnySpecification<T>();

      return left?.And(expression) ?? And(null, expression);
    }

    /// <summary>
    /// 条件And操作，支持规范对象参数
    /// 只有当条件为true时才执行And操作
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="left">左规范（可为null）</param>
    /// <param name="condition">条件</param>
    /// <param name="right">当条件为true时要And的规范</param>
    /// <returns>组合后的规范（非null）</returns>
    public static ISpecification<T> AndIf<T>(this ISpecification<T>? left, bool condition, ISpecification<T> right)
    {
      if (!condition)
        return left ?? new AnySpecification<T>();

      return left?.And(right) ?? right;
    }

    #endregion

    #region Or扩展方法

    /// <summary>
    /// Or操作，支持表达式参数
    /// 如果左规范为null，创建新的表达式规范；否则返回Or组合
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="left">左规范（可为null）</param>
    /// <param name="expression">右侧表达式</param>
    /// <returns>组合后的规范（非null）</returns>
    public static ISpecification<T> Or<T>(this ISpecification<T>? left, Expression<Func<T, bool>> expression)
    {
      if (expression == null)
        throw new ArgumentNullException(nameof(expression));

      var rightSpecification = Specification<T>.Eval(expression);

      if (left == null)
        return rightSpecification;

      return new OrSpecification<T>(left, rightSpecification);
    }

    /// <summary>
    /// Or操作，支持规范对象参数
    /// 如果左规范为null，返回右规范；否则返回Or组合
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="left">左规范（可为null）</param>
    /// <param name="right">右规范（不可为null）</param>
    /// <returns>组合后的规范（非null）</returns>
    public static ISpecification<T> Or<T>(this ISpecification<T>? left, ISpecification<T> right)
    {
      if (right == null)
        throw new ArgumentNullException(nameof(right));

      if (left == null)
        return right;

      return new OrSpecification<T>(left, right);
    }

    /// <summary>
    /// 条件Or操作，支持表达式参数
    /// 只有当条件为true时才执行Or操作
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="left">左规范（可为null）</param>
    /// <param name="condition">条件</param>
    /// <param name="expression">当条件为true时要Or的表达式</param>
    /// <returns>组合后的规范（非null）</returns>
    public static ISpecification<T> OrIf<T>(this ISpecification<T>? left, bool condition, Expression<Func<T, bool>> expression)
    {
      if (!condition)
        return left ?? new AnySpecification<T>();

      return left?.Or(expression) ?? Or(null, expression);
    }

    /// <summary>
    /// 条件Or操作，支持规范对象参数
    /// 只有当条件为true时才执行Or操作
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="left">左规范（可为null）</param>
    /// <param name="condition">条件</param>
    /// <param name="right">当条件为true时要Or的规范</param>
    /// <returns>组合后的规范（非null）</returns>
    public static ISpecification<T> OrIf<T>(this ISpecification<T>? left, bool condition, ISpecification<T> right)
    {
      if (!condition)
        return left ?? new AnySpecification<T>();

      return left?.Or(right) ?? right;
    }

    #endregion

    #region 便捷方法

    /// <summary>
    /// 获取规范的表达式，如果规范为null则返回"总是true"的表达式
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="specification">规范（可为null）</param>
    /// <returns>LINQ表达式</returns>
    public static Expression<Func<T, bool>> GetExpressionOrDefault<T>(this ISpecification<T>? specification)
    {
      return specification?.GetExpression() ?? (x => true);
    }

    /// <summary>
    /// 获取规范的表达式，如果规范为null则返回指定的默认表达式
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="specification">规范（可为null）</param>
    /// <param name="defaultExpression">默认表达式</param>
    /// <returns>LINQ表达式</returns>
    public static Expression<Func<T, bool>> GetExpressionOrDefault<T>(this ISpecification<T>? specification, Expression<Func<T, bool>> defaultExpression)
    {
      return specification?.GetExpression() ?? defaultExpression;
    }

    /// <summary>
    /// 如果规范为null，返回"总是true"的规范
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="specification">规范（可为null）</param>
    /// <returns>非null的规范</returns>
    public static ISpecification<T> OrAny<T>(this ISpecification<T>? specification)
    {
      return specification ?? new AnySpecification<T>();
    }

    /// <summary>
    /// 如果规范为null，返回"总不满足"的规范
    /// </summary>
    /// <typeparam name="T">规范应用的对象类型</typeparam>
    /// <param name="specification">规范（可为null）</param>
    /// <returns>非null的规范</returns>
    public static ISpecification<T> OrNone<T>(this ISpecification<T>? specification)
    {
      return specification ?? new NoneSpecification<T>();
    }

    #endregion
  }
}