﻿using MySvc.Framework.Infrastructure.Serilog.Sinks;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog.Formatting;
using Serilog.Sinks.PeriodicBatching;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;

namespace MySvc.Framework.Infrastructure.Serilog
{
    /// <summary>
    /// Adds the WriteTo.Email() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationEmailExtensions
    {
        const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";
        const int DefaultBatchPostingLimit = 100;
        static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Adds a sink that sends log events via email.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="fromEmail">The email address emails will be sent from</param>
        /// <param name="toEmail">The email address emails will be sent to</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="mailSubject">The subject, can be a plain string or a template such as {Timestamp} [{Level}] occurred.</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        /// <exception cref="System.ArgumentNullException">loggerConfiguration
        /// or
        /// fromEmail
        /// or
        /// toEmail</exception>
        public static LoggerConfiguration Email(
            this LoggerSinkConfiguration loggerConfiguration,
            string fromEmail,
            string toEmail,
            string outputTemplate = DefaultOutputTemplate,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null,
            string mailSubject = EmailConnectionInfo.DefaultSubject)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (fromEmail == null) throw new ArgumentNullException("fromEmail");
            if (toEmail == null) throw new ArgumentNullException("toEmail");

            var connectionInfo = new EmailConnectionInfo
            {
                FromEmail = fromEmail,
                ToEmail = toEmail,
                EmailSubject = mailSubject
            };

            return Email(loggerConfiguration, connectionInfo, outputTemplate, restrictedToMinimumLevel, batchPostingLimit, period, formatProvider);
        }

        /// <summary>
        /// Adds a sink that sends log events via email.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="fromEmail">The email address emails will be sent from</param>
        /// <param name="toEmail">The email address emails will be sent to</param>
        /// <param name="mailServer">The SMTP email server to use</param>
        /// <param name="networkCredential">The network credentials to use to authenticate with mailServer</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="mailSubject">The subject, can be a plain string or a template such as {Timestamp} [{Level}] occurred.</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        /// <exception cref="System.ArgumentNullException">loggerConfiguration
        /// or
        /// fromEmail
        /// or
        /// toEmail</exception>
        public static LoggerConfiguration Email(
            this LoggerSinkConfiguration loggerConfiguration,
            string fromEmail,
            string toEmail,
            string mailServer,
            ICredentialsByHost networkCredential = null,
            string outputTemplate = DefaultOutputTemplate,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null,
            string mailSubject = EmailConnectionInfo.DefaultSubject)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (fromEmail == null) throw new ArgumentNullException("fromEmail");
            if (toEmail == null) throw new ArgumentNullException("toEmail");

            var connectionInfo = new EmailConnectionInfo
            {
                FromEmail = fromEmail,
                ToEmail = toEmail,
                MailServer = mailServer,
                NetworkCredentials = networkCredential,
                EmailSubject = mailSubject
            };

            return Email(loggerConfiguration, connectionInfo, outputTemplate, restrictedToMinimumLevel, batchPostingLimit, period, formatProvider);
        }

        /// <summary>
        /// Adds a sink that sends log events via email.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="fromEmail">The email address emails will be sent from</param>
        /// <param name="toEmails">The email addresses emails will be sent to</param>
        /// <param name="mailServer">The SMTP email server to use</param>
        /// <param name="networkCredential">The network credentials to use to authenticate with mailServer</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="mailSubject">The subject, can be a plain string or a template such as {Timestamp} [{Level}] occurred.</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        /// <exception cref="System.ArgumentNullException">loggerConfiguration
        /// or
        /// fromEmail
        /// or
        /// toEmails</exception>
        public static LoggerConfiguration Email(
            this LoggerSinkConfiguration loggerConfiguration,
            string fromEmail,
            IEnumerable<string> toEmails,
            string mailServer,
            ICredentialsByHost networkCredential = null,
            string outputTemplate = DefaultOutputTemplate,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null,
            string mailSubject = EmailConnectionInfo.DefaultSubject)
        {
            if (loggerConfiguration == null) throw new ArgumentNullException("loggerConfiguration");
            if (fromEmail == null) throw new ArgumentNullException("fromEmail");
            if (toEmails == null) throw new ArgumentNullException("toEmails");

            var connectionInfo = new EmailConnectionInfo
            {
                FromEmail = fromEmail,
                ToEmail = string.Join(";", toEmails),
                MailServer = mailServer,
                NetworkCredentials = networkCredential,
                EmailSubject = mailSubject
            };

            return Email(loggerConfiguration, connectionInfo, outputTemplate, restrictedToMinimumLevel, batchPostingLimit, period, formatProvider, mailSubject);
        }

        /// <summary>
        /// Adds a sink that sends log events via email.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="connectionInfo">The connection info used for</param>
        /// <param name="outputTemplate">A message template describing the format used to write to the sink.
        /// the default is "{Timestamp} [{Level}] {Message}{NewLine}{Exception}".</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="formatProvider">Supplies culture-specific formatting information, or null.</param>
        /// <param name="mailSubject">The subject, can be a plain string or a template such as {Timestamp} [{Level}] occurred.</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        /// <exception cref="System.ArgumentNullException">connectionInfo</exception>
        public static LoggerConfiguration Email(
            this LoggerSinkConfiguration loggerConfiguration,
            EmailConnectionInfo connectionInfo,
            string outputTemplate = DefaultOutputTemplate,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = DefaultBatchPostingLimit,
            TimeSpan? period = null,
            IFormatProvider formatProvider = null,
            string mailSubject = EmailConnectionInfo.DefaultSubject)
        {
            if (connectionInfo == null) throw new ArgumentNullException("connectionInfo");

            if (!string.IsNullOrEmpty(connectionInfo.EmailSubject))
            {
                mailSubject = connectionInfo.EmailSubject;
            }

            var batchingPeriod = period ?? DefaultPeriod;
            var textFormatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);
            var mailSubjectFormatter = new MessageTemplateTextFormatter(mailSubject, formatProvider);

            var batchingOptions = new PeriodicBatchingSinkOptions
            {
                BatchSizeLimit = batchPostingLimit,
                Period = batchingPeriod,
                EagerlyEmitFirstEvent = false,  // set default to false, not usable for emailing
                QueueLimit = 10000
            };
            var batchingSink = new PeriodicBatchingSink(new EmailSink(connectionInfo, textFormatter, mailSubjectFormatter), batchingOptions);

            return loggerConfiguration.Sink(batchingSink, restrictedToMinimumLevel);
        }

        /// <summary>
        /// Adds a sink that sends log events via email.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="connectionInfo">The connection info used for</param>
        /// <param name="textFormatter">ITextFormatter implementation to write log entry to email.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="batchPostingLimit">The maximum number of events to post in a single batch.</param>
        /// <param name="period">The time to wait between checking for event batches.</param>
        /// <param name="mailSubject">The subject, can be a plain string or a template such as {Timestamp} [{Level}] occurred.</param>
        /// <returns>
        /// Logger configuration, allowing configuration to continue.
        /// </returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        /// <exception cref="System.ArgumentNullException">connectionInfo
        /// or
        /// textFormatter</exception>
        public static LoggerConfiguration Email(
            this LoggerSinkConfiguration loggerConfiguration,
            EmailConnectionInfo connectionInfo,
            ITextFormatter textFormatter,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            int batchPostingLimit = DefaultBatchPostingLimit,
            TimeSpan? period = null,
            string mailSubject = EmailConnectionInfo.DefaultSubject)
        {
            if (connectionInfo == null) throw new ArgumentNullException("connectionInfo");
            if (textFormatter == null) throw new ArgumentNullException("textFormatter");

            ITextFormatter mailSubjectFormatter = new MessageTemplateTextFormatter(mailSubject, null);

            var batchingPeriod = period ?? DefaultPeriod;

            var batchingOptions = new PeriodicBatchingSinkOptions
            {
                BatchSizeLimit = batchPostingLimit,
                Period = batchingPeriod,
                EagerlyEmitFirstEvent = false,  // set default to false, not usable for emailing
                QueueLimit = 10000
            };
            var batchingSink = new PeriodicBatchingSink(new EmailSink(connectionInfo, textFormatter, mailSubjectFormatter), batchingOptions);

            return loggerConfiguration.Sink(batchingSink, restrictedToMinimumLevel);
        }
    }
}
