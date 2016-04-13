using System;
using ClassLibrary1.Framework;
using Newtonsoft.Json;

namespace ClassLibrary1.Events
{
    public class CustomerAddressChanged : DomainEvent
    {
        [JsonConstructor]
        public CustomerAddressChanged(Guid id, string address) : base(id)
        {
            this.Address = address;
        }

        public string Address { get; }
    }
}