using System;
using System.Collections.Generic;
using System.Text;
using MySvc.Framework.Infrastructure.Crosscutting.Jobs;
using Hangfire;

namespace MySvc.Framework.Infrastructure.Job.Hangfire
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

        public string DelayedJob<TJob, TParam>(TParam param, TimeSpan delay) where TJob : IJob<TParam>
        {
            return BackgroundJob.Schedule<TJob>((job) => job.ExecuteAsync(param), delay);

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

        public string Enqueue<TJob, TParam>( TParam param) where TJob : IJob<TParam>
        {
            return BackgroundJob.Enqueue<TJob>((job) => job.ExecuteAsync(param));
        }

        /// <summary>
        /// 周期任务
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="obj"></param>
        /// <param name="cronExpression">cron表达式</param>
        /// <param name="recurringJobId">周期任务的jobid</param>
        public string Recurring<TParam>( TParam obj, string cronExpression, string recurringJobId = null)
        {
            if (string.IsNullOrEmpty(recurringJobId))
            {
                recurringJobId = Guid.NewGuid().ToString();
            }
            RecurringJob.AddOrUpdate<IRecurringJob<TParam>>(recurringJobId, job => job.ExecuteAsync(recurringJobId, obj), cronExpression);

            return recurringJobId;
        }

        public string Recurring<TParam>(TParam obj, string cronExpression, TimeZoneInfo timeZoneInfo, string recurringJobId = null)
        {
            if (string.IsNullOrEmpty(recurringJobId))
            {
                recurringJobId = Guid.NewGuid().ToString();
            }
            RecurringJob.AddOrUpdate<IRecurringJob<TParam>>(recurringJobId, job => job.ExecuteAsync(recurringJobId, obj), cronExpression, timeZoneInfo);

            return recurringJobId;
        }

        public string Recurring<TJob, TParam>(TParam param, string cronExpression, string recurringJobId = null) where TJob : IRecurringJob<TParam>
        {
            if (string.IsNullOrEmpty(recurringJobId))
            {
                recurringJobId = Guid.NewGuid().ToString();
            }
            RecurringJob.AddOrUpdate<TJob>(recurringJobId, job => job.ExecuteAsync(recurringJobId, param), cronExpression);

            return recurringJobId;
        }

        public string Recurring<TJob, TParam>(TParam param, string cronExpression, TimeZoneInfo timeZoneInfo,
            string recurringJobId = null) where TJob : IRecurringJob<TParam>
        {
            if (string.IsNullOrEmpty(recurringJobId))
            {
                recurringJobId = Guid.NewGuid().ToString();
            }
            RecurringJob.AddOrUpdate<TJob>(recurringJobId, job => job.ExecuteAsync(recurringJobId, param), cronExpression, timeZoneInfo);

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
