using System.Collections.Generic;
using ClassLibrary1.Domain;
using ClassLibrary1.Events;
using ClassLibrary1.Framework;
using FluentAssertions;
using Xunit;

namespace ClassLibrary1
{
    public class Tests
    {

        [Fact]
        public void SimpleTest()
        {
            var jsonCreate = "{ \"Name\": \"Max\", \"Address\": \"Berlin\" }";
            var jsonAddressChanged = "{ \"Address\": \"New York\" }";

            var dbObjects = new List<DbObject>
            {
                new DbObject {Type = typeof(CustomerCreated).Name, Payload = jsonCreate},
                new DbObject {Type = typeof(CustomerAddressChanged).Namespace, Payload = jsonAddressChanged}
            };

            // `dbObjects` is my mock data!!!!!!!!!!
            // I have to create `domainEvents` from `dbObjects`
            // I want to call convert method which converts to the correct event

            // DomainEvent e = MyConverter.Convert(...)
            var domainEvents = new List<DomainEvent>();

            var customer = new Customer();
            foreach (var domainEvent in domainEvents) {
                customer.Apply(domainEvent);
            }

            customer.Name.Should().Be("Max");
            customer.Address.Should().Be("New York");
        }
    }

    class DbObject
    {
        public string Type { get; set; }
        public string Payload { get; set; } 
    }

    // Just an idea
    public class MyConverter
    {
        public T Convert<T>(dynamic source) where T : class // or DomainEvent
        {
            // TODO
            return null;
        }
    }
}