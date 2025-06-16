using MySvc.Framework.Domain.Core.Impl;
using MySvc.Framework.Infrastructure.Crosscutting.Helpers;
using System;
namespace Domain.Core.Tests
{
    public class ContactInfo: ValueObject<ContactInfo>
    {
        public ContactInfo(string contactPerson, string contactPhone, string contactEmail, Address contactAddress)
        {
            if(string.IsNullOrWhiteSpace(contactPerson)) throw  new ArgumentNullException(nameof(contactPerson));
            if (string.IsNullOrWhiteSpace(contactEmail)) throw new ArgumentNullException(nameof(contactEmail));

            this.ContactPerson = contactPerson;
            this.ContactPhone = contactPhone;
            this.ContactEmail = contactEmail;
            this.ContactAddress = contactAddress;
        }

        public string ContactPerson { get; private set; }
        public string ContactPhone { get; private set; }
        public string ContactEmail { get; private set; }
        public Address ContactAddress { get; private set; }

    }
}
