using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers
{
    /// <summary>
    /// 检查器
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Responsible for verifying methods arguments.
        /// </summary>
        public static class Argument {
            
            /// <summary>
            /// Assert that a string is neither null nor empty.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">argument name</param>
            /// <exception cref="ArgumentException">If the argument is either null or empty.</exception>
            [DebuggerStepThrough]
            public static void IsNotNullOrEmpty(string argument, string argumentName)
            {
                if (String.IsNullOrEmpty(argument))
                {
                    throw new ArgumentException(CheckResources.ArgumentCannotBeNullOrEmptyString.FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// Assert that a given string has the exact informed length.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <param name="expectedLength">The expected length of the string.</param>
            /// <exception cref="ArgumentOutOfRangeException">If the argument has not the informed length.</exception>
            public static void HasExactLength(string argument, string argumentName, int expectedLength)
            {
                if (argument == null || argument.Length != expectedLength)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Asserts that a byte array is not null nor empty.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <exception cref="ArgumentException">If the argument is either null or empty.</exception>
            [DebuggerStepThrough]
            public static void IsNotNullOrEmpty(byte[] argument, string argumentName)
            {
                if (argument == null || argument.Length <= 0)
                {
                    throw new ArgumentException(CheckResources.ArgumentCannotBeNullOrEmptyArray.FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// Asserts that a char array is not null nor empty.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <exception cref="ArgumentException">If the argument is either null or empty.</exception>
            [DebuggerStepThrough]
            public static void IsNotNullOrEmpty(char[] argument, string argumentName)
            {
                if (argument == null || argument.Length <= 0)
                {
                    throw new ArgumentException(CheckResources.ArgumentCannotBeNullOrEmptyArray.FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// Asserts that a collection is not null nor empty.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <exception cref="ArgumentException">If the argument is either null or empty.</exception>
            [DebuggerStepThrough]
            public static void IsNotNullOrEmpty<T>(IList<T> argument, string argumentName)
            {
                if (argument == null || argument.Count <= 0)
                {
                    throw new ArgumentException(CheckResources.ArgumentCannotBeNullOrEmptyCollection.FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// Asserts that an object is not null
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <exception cref="ArgumentException">If the argument is null.</exception>
            [DebuggerStepThrough]
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                {
                    throw new ArgumentException(CheckResources.ArgumentCannotBeNull.FormatWith(argumentName), argumentName);
                }
            }

            /// <summary>
            /// Asserts that the argument is between two given values.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <param name="min">Min value allowed</param>
            /// <param name="max">Max value allowed</param>
            /// <exception cref="ArgumentOutOfRangeException">If the argument not in the specified range.</exception>
            [DebuggerStepThrough]
            public static void IsInRange(int argument, string argumentName, int min, int max)
            {
                if (argument < min || argument > max)
                {
                    throw new ArgumentOutOfRangeException(argumentName, CheckResources.ArgumentMustBeInRange.FormatWith(argumentName, min, max));
                }
            }

            /// <summary>
            /// Asserts that a given datatime is not in the past.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <exception cref="ArgumentOutOfRangeException">If the argument is in the past.</exception>
            [DebuggerStepThrough]
            public static void IsNotInPast(DateTime argument, string argumentName)
            {
                if (argument < DateTime.Now)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Asserts that a given value is greater than zero.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <exception cref="ArgumentOutOfRangeException">If the argument is either zero or negative.</exception>
            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(TimeSpan argument, string argumentName)
            {
                if (argument <= TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Asserts that a given value is greater than zero.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <exception cref="ArgumentOutOfRangeException">If the argument is either zero or negative.</exception>
            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(int argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Asserts that a given value is greater than zero.
            /// </summary>
            /// <param name="argument">Argument</param>
            /// <param name="argumentName">Argument name</param>
            /// <exception cref="ArgumentOutOfRangeException">If the argument is either zero or negative.</exception>
            [DebuggerStepThrough]
            public static void IsNotNegativeOrZero(decimal argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            [DebuggerStepThrough]
            public static void IsNotNegative(decimal argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// 断言原参数值和目标参数值相等
            /// </summary>
            /// <param name="sourceAruemntValue">原参数值</param>
            /// <param name="destAruemntValue">目标参数值</param>
            /// <param name="sourceArgumentName">原参数名</param>
            /// <param name="destArgumentName">目标参数值</param>
            [DebuggerStepThrough]
            public static void IsEqual(int sourceAruemntValue, int destAruemntValue, string sourceArgumentName, string destArgumentName)
            {
                if (sourceAruemntValue != destAruemntValue)
                {
                    throw new ArgumentOutOfRangeException(sourceArgumentName, string.Format("{0}  must be equal {1}",sourceArgumentName,destArgumentName));
                }
            }
        }
    }
}