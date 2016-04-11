using System.Collections.Generic;
using ClassLibrary1.Domain;
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
        /// <summary>
        /// This is the string representation of the 
        /// </summary>
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