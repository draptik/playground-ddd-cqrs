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
            var jsonAddressChanged = "{ \"Address\": \"New York\" }";

            // this is my EventStore
            var dbObjects = new List<DbObject>
            {
                new DbObject {Type = typeof (CustomerCreated).Name, Payload = jsonCreate},
                new DbObject {Type = typeof (CustomerAddressChanged).Namespace, Payload = jsonAddressChanged}
            };


            foreach (var dbObject in dbObjects) {
                var payloadExpando = JsonConvert.DeserializeObject<ExpandoObject>(dbObject.Payload, new ExpandoObjectConverter());
                // Ok, the payload is an expando-object now...
                //
                // dbObject has the type as string.
                //
                // How do I convert the payload/expando object to the correct type?
                

                var domainEvents = new List<DomainEvent>();


                var customer = new Customer();
                foreach (var domainEvent in domainEvents) {
                    customer.Apply(domainEvent);
                }

                customer.Name.Should().Be("Max");
                customer.Address.Should().Be("New York");
            }
        }

        public class DbObject
        {
            public string Type { get; set; }
            public string Payload { get; set; }
        }

        // Just an idea
        public static class MyConverter
        {
            public static T Convert<T>(dynamic source) where T : class, new() // or DomainEvent
            {
                T t = new T();
                Update(source, t);
                return t;
            }

            private static void Update<T>(dynamic source, T o) where T : class
            {

            }
        }
    }
}