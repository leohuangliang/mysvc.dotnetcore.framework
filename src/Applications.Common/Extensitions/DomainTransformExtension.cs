using System.Collections.Generic;
using System.Linq;
using Capmarvel.Framework.Domain.Common.Models;
using Capmarvel.Framework.Domain.Common.Models.CustomeQuery;
using Capmarvel.Framework.Domain.Common.Models.CustomeQuery.Exressions;
using Capmarvel.Framework.Domain.Common.Models.CustomField;
using Capmarvel.Framework.Domain.Common.Models.CustomField.Inputs;
using VM = Capmarvel.Framework.Applications.Common.Models;

namespace Capmarvel.Framework.Applications.Common.Extensitions
{
    /// <summary>
    /// 公共类型 DTO 转Domain的通用方法
    /// </summary>
    public static class DomainTransformExtension
    {
        /// <summary>
        /// DTO的AccountInfo转换为Domian的AccountInfo
        /// </summary>
        public static AccountInfo ToDomain(this VM.AccountInfo account)
        {
            return account == null ? null : new AccountInfo(account.TenantCode, account.UserName);
        }

        /// <summary>
        /// DTO的Operation转换为Domian的Operation
        /// </summary>
        public static Operation ToDomain(this VM.Operation operation)
        {
            return operation == null ? null : new Operation(operation.Operator.ToDomain(), operation.Time);
        }

        /// <summary>
        /// DTO的Operator转换为Domian的Operator
        /// </summary>
        /// <param name="optor">操作者</param>
        public static Operator ToDomain(this VM.Operator optor)
        {
            return optor == null ? null : new Operator(optor.Code, optor.Name, optor.OperatorType);
        }

        /// <summary>
        /// DTO的CustomFieldDefinition转换为Domian的CustomFieldDefinition
        /// </summary>
        /// <param name="customFieldDefinition">自定义字段的定义</param>
        public static CustomFieldDefinition ToDomain(this VM.CustomField.CustomFieldDefinition customFieldDefinition)
        {
            return new CustomFieldDefinition(customFieldDefinition.Name, customFieldDefinition.IsRequired, customFieldDefinition.FieldInput.ToDomain());
        }

        /// <summary>
        /// DTO的CustomFieldInput转换为Domian的CustomFieldInput
        /// </summary>
        public static CustomFieldInput ToDomain(
            this VM.CustomField.Inputs.CustomFieldInput input)
        {
            if (input is VM.CustomField.Inputs.CustomFieldTextInput textInput)
            {
                return new CustomFieldTextInput(textInput.DefaultValue, textInput.Tips);
            }

            if (input is VM.CustomField.Inputs.CustomFieldNumberInput numberInput)
            {
                return new CustomFieldNumberInput(numberInput.DefaultValue, numberInput.Tips);
            }

            if (input is VM.CustomField.Inputs.CustomFieldDatetimeInput datetimeInput)
            {
                return new CustomFieldDatetimeInput(datetimeInput.DefaultValue, datetimeInput.Tips);
            }

            if (input is VM.CustomField.Inputs.CustomFieldSingleChoiceInput singleChoiceInput)
            {
                return new CustomFieldSingleChoiceInput(singleChoiceInput.DefaultValue, singleChoiceInput.Tips, singleChoiceInput.Options?.ToList() ?? new List<string>());
            }

            if (input is VM.CustomField.Inputs.CustomFieldMultiChoiceInput multiChoiceInput)
            {
                return new CustomFieldMultiChoiceInput(multiChoiceInput.DefaultValue, multiChoiceInput.Tips, multiChoiceInput.Options?.ToList() ?? new List<string>());
            }
            
            return null;
        }

        /// <summary>
        /// DTO的CustomeQueryField转换为Domian的CustomeQueryField
        /// </summary>
        public static CustomeQueryField ToDomain(this VM.CustomQuery.CustomeQueryField field)
        {
            return new CustomeQueryField(field.Name, field.DateType, field.FieldType);
        }

        public static CustomeQueryGroupExpression ToDomain(
            this VM.CustomQuery.Exressions.CustomeQueryGroupExpression expression)
        {
            VM.CustomQuery.Exressions.CustomeQueryExpression customeQueryExpression = expression;
            return (CustomeQueryGroupExpression)customeQueryExpression.ToDomain();
        }

        public static CustomeQuerySingleValue<T> ToDomain<T>(this VM.CustomQuery.CustomeQuerySingleValue<T> value)
        {
            return new CustomeQuerySingleValue<T>(value.Value);
        }

        public static CustomeQueryMultiValue<T> ToDomain<T>(this VM.CustomQuery.CustomeQueryMultiValue<T> value)
        {
            return new CustomeQueryMultiValue<T>(value.Values);
        }

        public static CustomeQueryRangeValue<T> ToDomain<T>(this VM.CustomQuery.CustomeQueryRangeValue<T> value)
        {
            return new CustomeQueryRangeValue<T>(value.LeftValue, value.RightValue);
        }

        /// <summary>
        /// DTO的CustomeQueryExpression转换为Domian的CustomeQueryExpression
        /// </summary>
        public static CustomeQueryExpression ToDomain(
            this VM.CustomQuery.Exressions.CustomeQueryExpression expression)
        {
            if (expression is VM.CustomQuery.Exressions.CustomeQueryGroupExpression groupExpression)
            {
                return new CustomeQueryGroupExpression(groupExpression.Expressions?.Select(x => x.ToDomain()).ToList(), groupExpression.LogicalOperators);
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryBoolNormalExpression boolNormalExpression)
            {
                return new CustomeQueryBoolNormalExpression(boolNormalExpression.Field.ToDomain(), boolNormalExpression.RelationalOperator, boolNormalExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryIntNormalExpression intNormalExpression)
            {
                return new CustomeQueryIntNormalExpression(intNormalExpression.Field.ToDomain(), intNormalExpression.RelationalOperator, intNormalExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryIntRangeExpression intRangeExpression)
            {
                return new CustomeQueryIntRangeExpression(intRangeExpression.Field.ToDomain(), intRangeExpression.RelationalOperator, intRangeExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryIntMultiMatchExpression intMultiMatchExpression)
            {
                return new CustomeQueryIntMultiMatchExpression(intMultiMatchExpression.Field.ToDomain(), intMultiMatchExpression.RelationalOperator, intMultiMatchExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryDecimalNormalExpression decimalNormalExpression)
            {
                return new CustomeQueryDecimalNormalExpression(decimalNormalExpression.Field.ToDomain(), decimalNormalExpression.RelationalOperator, decimalNormalExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryDecimalRangeExpression decimalRangeExpression)
            {
                return new CustomeQueryDecimalRangeExpression(decimalRangeExpression.Field.ToDomain(), decimalRangeExpression.RelationalOperator, decimalRangeExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryDecimalMultiMatchExpression decimalMultiMatchExpression)
            {
                return new CustomeQueryDecimalMultiMatchExpression(decimalMultiMatchExpression.Field.ToDomain(), decimalMultiMatchExpression.RelationalOperator, decimalMultiMatchExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryDateTimeNormalExpression datetimeNormalExpression)
            {
                return new CustomeQueryDateTimeNormalExpression(datetimeNormalExpression.Field.ToDomain(), datetimeNormalExpression.RelationalOperator, datetimeNormalExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryDateTimeRangeExpression datetimeRangeExpression)
            {
                return new CustomeQueryDateTimeRangeExpression(datetimeRangeExpression.Field.ToDomain(), datetimeRangeExpression.RelationalOperator, datetimeRangeExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryDateTimeMultiMatchExpression datetimeMultiMatchExpression)
            {
                return new CustomeQueryDateTimeMultiMatchExpression(datetimeMultiMatchExpression.Field.ToDomain(), datetimeMultiMatchExpression.RelationalOperator, datetimeMultiMatchExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryStringNormalExpression stringNormalExpression)
            {
                return new CustomeQueryStringNormalExpression(stringNormalExpression.Field.ToDomain(), stringNormalExpression.RelationalOperator, stringNormalExpression.Value.ToDomain());
            }

            if (expression is VM.CustomQuery.Exressions.CustomeQueryStringMultiMatchExpression stringMultiMatchExpression)
            {
                return new CustomeQueryStringMultiMatchExpression(stringMultiMatchExpression.Field.ToDomain(), stringMultiMatchExpression.RelationalOperator, stringMultiMatchExpression.Value.ToDomain());
            }

            return null;
        }
    }
}
