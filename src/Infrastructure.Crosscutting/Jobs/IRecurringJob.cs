using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Jobs
{
    /// <summary>
    /// 周期任务
    /// </summary>
    /// <typeparam name="TParam"></typeparam>
    public interface IRecurringJob<TParam>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recurringJobId"></param>
        /// <param name="jobParam"></param>
        /// <returns></returns>
        Task ExecuteAsync(string recurringJobId, TParam jobParam);
    }
}
