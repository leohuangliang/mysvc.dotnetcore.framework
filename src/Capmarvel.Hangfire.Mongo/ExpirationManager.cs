﻿using System;
using System.Threading;
using Hangfire.Logging;
using Hangfire.Mongo.Database;
using Hangfire.Mongo.Dto;
using Hangfire.Server;
using MongoDB.Driver;

namespace Hangfire.Mongo
{
    /// <summary>
    /// Represents Hangfire expiration manager for Mongo database
    /// </summary>
    public class ExpirationManager : IBackgroundProcess, IServerComponent
    {
        private static readonly ILog Logger = LogProvider.For<ExpirationManager>();

        private readonly HangfireDbContext _dbContext;
        private readonly TimeSpan _checkInterval;

        /// <summary>
        /// Constructs expiration manager with one hour checking interval
        /// </summary>
        /// <param name="dbContext">MongoDB storage</param>
        public ExpirationManager(HangfireDbContext dbContext)
            : this(dbContext, TimeSpan.FromHours(1))
        {
        }

        /// <summary>
        /// Constructs expiration manager with specified checking interval
        /// </summary>
        /// <param name="dbContext">MongoDB storage</param>
        /// <param name="checkInterval">Checking interval</param>
        public ExpirationManager(HangfireDbContext dbContext, TimeSpan checkInterval)
        {
            _dbContext = dbContext;
            _checkInterval = checkInterval;
        }

        /// <summary>
        /// Run expiration manager to remove outdated records
        /// </summary>
        /// <param name="context">Background processing context</param>
        public void Execute(BackgroundProcessContext context)
        {
            Execute(context.CancellationToken);
        }

        /// <summary>
        /// Run expiration manager to remove outdated records
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public void Execute(CancellationToken cancellationToken)
        {
            DateTime now = DateTime.UtcNow;

            Logger.DebugFormat("Removing outdated records from table '{0}'...",
                _dbContext.JobGraph.CollectionNamespace.CollectionName);

            _dbContext
                .JobGraph
                .OfType<ExpiringJobDto>().DeleteMany(Builders<ExpiringJobDto>.Filter.Lt(_ => _.ExpireAt, now));


            cancellationToken.WaitHandle.WaitOne(_checkInterval);
        }

        /// <summary>
        /// Returns text representation of the object
        /// </summary>
        public override string ToString()
        {
            return "Mongo Expiration Manager";
        }
    }
}