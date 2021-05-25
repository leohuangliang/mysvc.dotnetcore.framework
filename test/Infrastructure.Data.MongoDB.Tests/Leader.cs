using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.MongoDB.Tests
{
    public abstract class Leader : Person
    {
        protected Leader(string name) : base(name)
        {
            this.LeaderName = name;
        }

        public string LeaderName { get; protected set; }
    }

    public class GroupLeader : Leader
    {
        public GroupLeader(string name) : base(name)
        {
        }
    }

    public class CompanyLeader : Leader
    {
        public CompanyLeader(string name) : base(name)
        {
        }
    }
}
