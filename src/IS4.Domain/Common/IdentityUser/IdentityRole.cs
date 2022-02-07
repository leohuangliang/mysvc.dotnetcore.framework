using MySvc.Framework.Domain.Core.Attributes;
using MySvc.Framework.Domain.Core.Impl;

namespace MySvc.Framework.IS4.Domain.Common.IdentityUser
{
    [AggregateRootName("Roles")]
    public  class IdentityRole : AggregateRoot
    {
        public IdentityRole()
        {
            
        }

        public IdentityRole(string roleName) : this()
        {
            this.Name = roleName;
        }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public override string ToString() => this.Name;
    }
}
