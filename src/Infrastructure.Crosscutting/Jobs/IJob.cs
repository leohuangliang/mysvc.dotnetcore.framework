﻿using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Crosscutting.Jobs
    {
        /// <summary>
        /// 任务
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        public interface IJob<TParam>
        {
            Task ExecuteAsync(TParam jobParam);
        }


    }
