using System.ComponentModel;
using System.Net;

namespace MySvc.Framework.Infrastructure.Serilog.Sinks
{
    /// <summary>
    /// Connection information for use by the Email sink.
    /// </summary>
    public class EmailConnectionInfo
    {
        /// <summary>
        /// The default port used by for SMTP transfer.
        /// </summary>
        const int DefaultPort = 25;

        /// <summary>
        /// The default subject used for email messages.
        /// </summary>
        public const string DefaultSubject = "Log Email";

        /// <summary>
        /// Constructs the <see cref="EmailConnectionInfo"/> with the default port and default email subject set.
        /// </summary>
        public EmailConnectionInfo()
        {
            Port = DefaultPort;
            EmailSubject = DefaultSubject;
            IsBodyHtml = false;
        }

        /// <summary>
        /// Gets or sets the credentials used for authentication.
        /// </summary>
        public ICredentialsByHost NetworkCredentials { get; set; }

        /// <summary>
        /// Gets or sets the port used for the connection.
        /// Default value is 25.
        /// </summary>
        [DefaultValue(DefaultPort)]
        public int Port { get; set; }

        /// <summary>
        /// The email address emails will be sent from.
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// The email address(es) emails will be sent to. Accepts multiple email addresses separated by comma or semicolon.
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// The subject to use for the email, this can be a template.
        /// </summary>
        [DefaultValue(DefaultSubject)]
        public string EmailSubject { get; set; }

        /// <summary>
        /// Flag as true to use SSL in the SMTP client.
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Provides a method that validates server certificates.
        /// </summary>
        /// <remarks>
        /// This only works on `netstandard1.3` with `MailKit`. If you
        /// are targeting `net45`+, you should add your validation to 
        /// `System.Net.ServicePointManager.ServerCertificateValidationCallback`
        /// manually.
        /// </remarks>
        public System.Net.Security.RemoteCertificateValidationCallback ServerCertificateValidationCallback { get; set; }

        /// <summary>
        /// The SMTP email server to use.
        /// </summary>
        public string MailServer { get; set; }

        /// <summary>
        /// Sets whether the body contents of the email is HTML. Defaults to false.
        /// </summary>
        public bool IsBodyHtml { get; set; }
    }
}
