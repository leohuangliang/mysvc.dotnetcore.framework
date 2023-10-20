using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Serilog.Debugging;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Sinks.PeriodicBatching;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace MySvc.Framework.Infrastructure.Serilog.Sinks
{
    public class EmailSink : IBatchedLogEventSink
    {
        readonly EmailConnectionInfo _connectionInfo;

        readonly MimeKit.InternetAddress _fromAddress;
        readonly IEnumerable<MimeKit.InternetAddress> _toAddresses;

        readonly ITextFormatter _textFormatter;

        readonly ITextFormatter _subjectFormatter;

        /// <summary>
        /// Construct a sink emailing with the specified details.
        /// </summary>
        /// <param name="connectionInfo">Connection information used to construct the SMTP client and mail messages.</param>
        /// <param name="textFormatter">Supplies culture-specific formatting information, or null.</param>
        /// <param name="subjectLineFormatter">The subject line formatter.</param>
        /// <exception cref="System.ArgumentNullException">connectionInfo</exception>
        public EmailSink(EmailConnectionInfo connectionInfo, ITextFormatter textFormatter, ITextFormatter subjectLineFormatter)
        {
            if (connectionInfo == null) throw new ArgumentNullException(nameof(connectionInfo));

            _connectionInfo = connectionInfo;
            _fromAddress = MimeKit.MailboxAddress.Parse(_connectionInfo.FromEmail);
            _toAddresses = connectionInfo
                .ToEmail
                .Split(",;".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(MimeKit.MailboxAddress.Parse)
                .ToArray();

            _textFormatter = textFormatter;
            _subjectFormatter = subjectLineFormatter;
        }

        private MimeKit.MimeMessage CreateMailMessage(string payload, string subject)
        {
            var mailMessage = new MimeKit.MimeMessage();
            mailMessage.From.Add(_fromAddress);
            mailMessage.To.AddRange(_toAddresses);
            mailMessage.Subject = subject;
            mailMessage.Body = _connectionInfo.IsBodyHtml
                ? new MimeKit.BodyBuilder { HtmlBody = payload }.ToMessageBody()
                : new MimeKit.BodyBuilder { TextBody = payload }.ToMessageBody();
            return mailMessage;
        }

        /// <summary>
        /// Emit a batch of log events, running asynchronously.
        /// </summary>
        /// <param name="events">The events to emit.</param>
        /// <remarks>Override either <see cref="PeriodicBatchingSink.EmitBatch"/> or <see cref="PeriodicBatchingSink.EmitBatchAsync"/>,
        /// not both.</remarks>
        public async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            if (events == null)
                throw new ArgumentNullException(nameof(events));

            var payload = new StringWriter();

            foreach (var logEvent in events)
            {
                _textFormatter.Format(logEvent, payload);
            }

            var subject = new StringWriter();
            _subjectFormatter.Format(events.OrderByDescending(e => e.Level).First(), subject);

            var mailMessage = CreateMailMessage(payload.ToString(), subject.ToString());

            try
            {
                using (var smtpClient = OpenConnectedSmtpClient())
                {
                    await smtpClient.SendAsync(mailMessage);
                    await smtpClient.DisconnectAsync(quit: true);
                }
            }
            catch (Exception ex)
            {
                SelfLog.WriteLine("Failed to send email: {0}", ex.ToString());
            }
        }

        public Task OnEmptyBatchAsync()
        {
            return Task.FromResult(false);
        }

        private SmtpClient OpenConnectedSmtpClient()
        {
            var smtpClient = new SmtpClient();
            if (!string.IsNullOrWhiteSpace(_connectionInfo.MailServer))
            {
                if (_connectionInfo.ServerCertificateValidationCallback != null)
                {
                    smtpClient.ServerCertificateValidationCallback += _connectionInfo.ServerCertificateValidationCallback;
                }

                smtpClient.Connect(
                    _connectionInfo.MailServer, _connectionInfo.Port,
                    useSsl: _connectionInfo.EnableSsl);

                if (_connectionInfo.NetworkCredentials != null)
                {
                    smtpClient.Authenticate(
                        Encoding.UTF8,
                        _connectionInfo.NetworkCredentials.GetCredential(
                            _connectionInfo.MailServer, _connectionInfo.Port, "smtp"));
                }
            }
            return smtpClient;
        }
    }
}
