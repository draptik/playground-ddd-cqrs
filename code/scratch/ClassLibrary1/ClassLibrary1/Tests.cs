using System;
using System.Collections.Generic;
using System.Dynamic;
using ClassLibrary1.Domain;
using ClassLibrary1.Events;
using ClassLibrary1.Framework;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xunit;

namespace ClassLibrary1
{
    public class Tests
    {

        [Fact]
        public void SimpleTest()
        {
            var jsonCreate = "{ \"Name\": \"Max\", \"Address\": \"Berlin\" }";
            var jsonAddressChanged = "{ \"Address\": \"Hamburg\" }";
            var jsonAddressChangedAgain = "{ \"Address\": \"New York\" }";

            // this is my EventStore
            var dbObjects = new List<DbObject>
            {
                new DbObject {Type = typeof (CustomerCreated).AssemblyQualifiedName, Payload = jsonCreate},
                new DbObject {Type = typeof (CustomerAddressChanged).AssemblyQualifiedName, Payload = jsonAddressChanged},
                new DbObject {Type = typeof (CustomerAddressChanged).AssemblyQualifiedName, Payload = jsonAddressChangedAgain}
            };

            var domainEvents = new List<DomainEvent>();
            foreach (var dbObject in dbObjects) {
                var payload = JsonConvert.DeserializeObject(dbObject.Payload, Type.GetType(dbObject.Type));
                domainEvents.Add(payload as DomainEvent);
            }

            var customer = new Customer();
            foreach (var domainEvent in domainEvents)
            {
                customer.Apply(domainEvent);
            }

            customer.Name.Should().Be("Max");
            customer.Address.Should().Be("New York");
        }

        public class DbObject
        {
            public string Type { get; set; }
            public string Payload { get; set; }
        }

        // Just an idea
        //public static class MyConverter
        //{
            

        //    public static T Convert<T>(dynamic source) where T : class, new() // or DomainEvent
        //    {
        //        T t = new T();
        //        Update(source, t);
        //        return t;
        //    }

        //    private static void Update<T>(dynamic source, T o) where T : class
        //    {

        //    }
        //}
    }
}