using MySvc.DotNetCore.Framework.Domain.Core.Impl;
using MySvc.DotNetCore.Framework.Infrastructure.Crosscutting.Helpers;
using System;
namespace Domain.Core.Tests
{
    public class ContactInfo: ValueObject<ContactInfo>
    {
        public ContactInfo(string contactPerson, string contactPhone, string contactEmail, Address contactAddress)
        {
            if(contactPerson.IsNullOrBlank()) throw  new ArgumentNullException(nameof(contactPerson));
            if (contactEmail.IsNullOrBlank()) throw new ArgumentNullException(nameof(contactEmail));

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