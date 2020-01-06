using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Capmarvel.Framework.Domain.Core.Impl;
using Capmarvel.Framework.Domain.Common.Models.CustomeQuery;

namespace Capmarvel.Framework.Domain.Common.ExpressionBuilders.CustomQuery
{
    /// <summary>
    /// 自定义查询构建对应的系统表达式树的统一管理类
    /// </summary>
    public class CustomQueryExpressionManager
    {
        private static IList<ICustomQueryFieldExpressionBuilder> _fieldExpressionBuilders = new List<ICustomQueryFieldExpressionBuilder>();

        private static IList<ICustomQueryLambdaExpressioneBuilder> _lambdaExpressioneBuilders = new List<ICustomQueryLambdaExpressioneBuilder>();

        /// <summary>
        /// 根据程序集名称, 注册 表示聚合对象的字段表达式的构建器
        /// </summary>
        /// <param name="assemblyNames">程序集名称</param>
        public static void RegisterCustomQueryFieldExpressionBuilders(List<string> assemblyNames = null)
        {
            var assemblys = GetSpecifiedAssemblysOrAll();

            foreach (var assembly in assemblys)
            {
                var fieldExpressionBuilderTypes = assembly.GetTypes().Where(t =>
                    t.GetInterfaces().Contains(typeof(ICustomQueryFieldExpressionBuilder))).ToList();

                fieldExpressionBuilderTypes.Select(x => Activator.CreateInstance(x) as ICustomQueryFieldExpressionBuilder)
                    .ToList()
                    .ForEach(RegisterCustomQueryFieldExpressionBuilder);
            }
        }

        /// <summary>
        /// 根据程序集名称, 注册 定义查询转系统lambda表达式树的构建器
        /// </summary>
        /// <param name="assemblyNames">程序集名称</param>
        public static void RegisterCustomQueryLambdaExpressioneBuilders(List<string> assemblyNames = null)
        {
            var assemblys = GetSpecifiedAssemblysOrAll();

            foreach (var assembly in assemblys)
            {
                var lambdaExpressioneBuilderTypes = assembly.GetTypes().Where(t => t.GetInterfaces()
                    .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICustomQueryLambdaExpressioneBuilder<>).GetGenericTypeDefinition())).ToList();
                
                lambdaExpressioneBuilderTypes.Select(x => Activator.CreateInstance(x) as ICustomQueryLambdaExpressioneBuilder)
                    .ToList()
                    .ForEach(RegisterCustomQueryLambdaExpressioneBuilder);
            }
        }

        /// <summary>
        /// 根据程序集名称，获取程序集对象。如果为空，则获取全部。
        /// </summary>
        /// <param name="assemblyNames">程序集名称</param>
        /// <returns>程序集</returns>
        private static List<Assembly> GetSpecifiedAssemblysOrAll(List<string> assemblyNames = null)
        {
            List<string> names = new List<string>();
            List<Assembly> assList = new List<Assembly>();
            if (assemblyNames == null || assemblyNames.Count == 0)
            {
                assList = AppDomain.CurrentDomain.GetAssemblies().ToList();

            }
            else
            {
                foreach (var assemblyName in assemblyNames)
                {
                    var ass = Assembly.Load(assemblyName);
                    if (ass != null)
                    {
                        assList.Add(ass);
                    }
                }
            }

            return assList;
        }

        /// <summary>
        /// 注册构建一个表示聚合对象的字段表达式的构建器
        /// </summary>
        /// <param name="builder">构建一个表示聚合对象的字段表达式的构建器</param>
        public static void RegisterCustomQueryFieldExpressionBuilder(ICustomQueryFieldExpressionBuilder builder)
        {
            if (!_fieldExpressionBuilders.Any(x => x.AggregateRootType == builder.AggregateRootType && x.Field == builder.Field))
            {
                _fieldExpressionBuilders.Add(builder);
            }
        }

        /// <summary>
        /// 注册自定义查询转系统lambda表达式树的构建器
        /// </summary>
        /// <param name="builder">自定义查询转系统lambda表达式树的构建器</param>
        public static void RegisterCustomQueryLambdaExpressioneBuilder(ICustomQueryLambdaExpressioneBuilder builder)
        {
            if (!_lambdaExpressioneBuilders.Any(x => x.AggregateRootType == builder.AggregateRootType && x.Field == builder.Field))
            {
                _lambdaExpressioneBuilders.Add(builder);
            }
        }

        /// <summary>
        /// 根据字段信息，获取特定的字段表达式
        /// </summary>
        /// <param name="parameterExpression">变量参数表达式， 表示x.Name的x</param>
        /// <param name="field">字段</param>
        public static Expression GetFieldExpression<T>(ParameterExpression parameterExpression, CustomeQueryField field)
            where T : AggregateRoot
        {
            var fieldExpressionBuilder = _fieldExpressionBuilders.FirstOrDefault(x => x.AggregateRootType == typeof(T) && x.Field == field);

            if (fieldExpressionBuilder != null)
            {
                return fieldExpressionBuilder.GetFieldExpression(parameterExpression);
            }

            //默认方式
            var property = typeof(T).GetProperty(field.Name);
            var fieldExression = Expression.Property(parameterExpression, property);

            return fieldExression;
        }

        /// <summary>
        /// 根据具体字段以及聚合根类型，获取特定的Lambda表达式构建器
        /// </summary>
        /// <typeparam name="T">聚合根类型</typeparam>
        /// <param name="field">字段信息</param>
        /// <returns>Lambda表达式构建器</returns>
        public static ICustomQueryLambdaExpressioneBuilder<T> GetLambdaExpressioneBuilder<T>(CustomeQueryField field)
            where T : AggregateRoot
        {
            var builder = (ICustomQueryLambdaExpressioneBuilder<T>)_lambdaExpressioneBuilders
                .Where(x => x.AggregateRootType == typeof(T))
                .FirstOrDefault(x => x.Field == field);
 
            return builder;
        }
    }
}
