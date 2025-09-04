﻿using System;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Crosscutting.Jobs
    {
        /// <summary>
        /// 任务
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        [Obsolete("已不再支持，请参考 Hangfire 官方支持", false)]
        public interface IJob<TParam>
        {
            Task ExecuteAsync(TParam jobParam);
        }


    }
