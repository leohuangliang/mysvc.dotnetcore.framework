using System;
using System.Collections.Generic;
using System.Text;

namespace MySvc.Framework.Infrastructure.Crosscutting.Options
{
    public class RabbitMQSettings
    {
        public required string HostName { get; set; }
        public required string Port { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }

        public required string VirtualHost { get; set; }

        public required string ExchangeName { get; set; }

        public required string SubscriptionClientName { get; set; }
    }
}
