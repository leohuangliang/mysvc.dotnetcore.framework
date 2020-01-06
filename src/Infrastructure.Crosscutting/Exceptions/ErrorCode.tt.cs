﻿//=======================================================================================
//
// Copyright (C) 2010-2011 Asia-Peak Technologies, Inc. All Rights Reserved.
// 
// All the code, text, graphics, media, design, programs and other works are
// protected by copyright law. Unauthorized reproduction or redistribution of them, 
// or any portion of them, are forbidden.
// 
//=======================================================================================
//---------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------

using System;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Exceptions
{    
    public static class ErrorCodes
    {
        public static class NumbericCodes
        {
        			/// <summary>
            /// 数据库更新时发生并发错误
            /// </summary>
            public const int ConcurrencyException = unchecked((int)0x800F3000);

        }

        public static class StringCodes
        {
        			/// <summary>
            /// 数据库更新时发生并发错误
            /// </summary>
            public const string ConcurrencyException = "800F3000";

        }

        public static string ToHex(this int value)
        {
            return "0x" + value.ToString("X");
        }
    }
}