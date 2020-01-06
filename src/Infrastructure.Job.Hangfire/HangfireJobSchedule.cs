using System;
using System.Collections.Generic;
using System.Text;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Jobs;
using Hangfire;

namespace MySvc.DotNetCore.Framework.Infrastructure.Job.Hangfire
{
    public class HangfireJobSchedule : IJobSchedule
    {
        /// <summary>
        /// 延迟任务
        /// </summary>
        /// <returns>jobId</returns>
        public string DelayedJob<TParam>(TParam obj, TimeSpan delay)
        {

            return BackgroundJob.Schedule<IJob<TParam>>((job) => job.ExecuteAsync(obj), delay);
        }


        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        public void DeleteJob(string jobId)
        {
            BackgroundJob.Delete(jobId);
        }

        /// <summary>
        /// 立即执行任务
        /// </summary>
        /// <returns>jobId</returns>
        public string Enqueue<TParam>(TParam obj)
        {
            return BackgroundJob.Enqueue<IJob<TParam>>((job) => job.ExecuteAsync(obj));
        }

        /// <summary>
        /// 周期任务
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="obj"></param>
        /// <param name="cronExpression">cron表达式</param>
        /// <param name="recurringJobId">周期任务的jobid</param>
        public string Recurring<TParam>(TParam obj, string cronExpression, string recurringJobId = null)
        {
            if (string.IsNullOrEmpty(recurringJobId))
            {
                recurringJobId = Guid.NewGuid().ToString();
            }
            RecurringJob.AddOrUpdate<IRecurringJob<TParam>>(recurringJobId, job => job.ExecuteAsync(recurringJobId, obj), cronExpression);

            return recurringJobId;
        }

        /// <summary>
        /// 删除周期任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        public void DeleteRecurringJob(string recurringJobId)
        {
            RecurringJob.RemoveIfExists(recurringJobId);
        }

        /// <summary>
        /// 触发周期任务
        /// </summary>
        /// <param name="recurringJobId"></param>
        public void TriggerRecurringJob(string recurringJobId)
        {
            RecurringJob.Trigger(recurringJobId);
        }
    }
}
