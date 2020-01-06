using DotNetCore.CAP.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.EventBus
{
    public class SubscribeAttribute : TopicAttribute
    {
        public SubscribeAttribute(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return base.Name;
        }
    }
}
