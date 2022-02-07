using System;
using System.Collections.Generic;
using System.Text;

namespace MySvc.Framework.Infrastructure.Crosscutting.Jobs
{
    public interface IJobSchedule
    {
        /// <summary>
        /// 延迟任务
        /// </summary>
        /// <returns>jobId</returns>
        string DelayedJob<TParam>(TParam obj, TimeSpan delay);

        string DelayedJob<TJob,TParam>(TParam param, TimeSpan delay) where TJob : IJob<TParam>;
        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <returns>jobId</returns>
        string Enqueue<TParam>(TParam obj);

        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <returns>jobId</returns>
        string Enqueue<TJob,TParam>(TParam param) where TJob : IJob<TParam>;

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        void DeleteJob(string jobId);

        /// <summary>
        /// 周期任务
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="obj"></param>
        /// <param name="cronExpression">cron表达式</param>
        /// <param name="recurringJobId">周期任务的jobid</param>
        string Recurring<TParam>(TParam obj, string cronExpression, string recurringJobId = null);

        /// <summary>
        /// 周期任务
        /// </summary>
        /// <typeparam name="TJob"></typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="cronExpression">cron表达式</param>
        /// <param name="recurringJobId">周期任务的jobid</param>
        string Recurring<TJob, TParam>(TParam param, string cronExpression, string recurringJobId = null) where TJob : IRecurringJob<TParam>;
        
        /// <summary>
        /// 删除周期任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        void DeleteRecurringJob(string recurringJobId);


        /// <summary>
        /// 触发周期任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        void TriggerRecurringJob(string recurringJobId);
    }
}
