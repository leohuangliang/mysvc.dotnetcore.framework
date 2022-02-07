using System;
using System.Collections.Generic;
using System.Text;

namespace MySvc.Framework.Infrastructure.Crosscutting.Options
{
    public class RabbitMQSettings
    {
        public string HostName { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string VirtualHost { get; set; }

        public string ExchangeName { get; set; }

        public string SubscriptionClientName { get; set; }
    }
}
