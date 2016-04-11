using System;
using Simple.Common;
using Newtonsoft.Json;
using System.Dynamic;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Simple.Eventstore
{
    public class EventWrapper
    {
        public Guid Id { get; private set; }

        public string Type { get; private set; }

        public DomainEvent Event { get; private set; }
        public dynamic Event2 { get; private set; }

        public Guid EventStreamId { get; private set; }

        public int EventNumber { get; private set; }
        private EventWrapper()
        {

        }
        

        public EventWrapper(DomainEvent @event, int eventNumber, Guid streamStateId)
        {
            this.Event = @event;
            this.Type = @event.GetType().ToString();
            this.EventNumber = eventNumber;
            this.EventStreamId = streamStateId;
            this.Id = Guid.NewGuid();
        }
        public static EventWrapper Create(Guid id, string type, string @event, Guid eventStreamId, int eventNumber)
        {
            var eventWrapper = new EventWrapper();
            eventWrapper.Id = id;
            eventWrapper.Type = type;
            eventWrapper.EventStreamId = eventStreamId;
            eventWrapper.EventNumber = eventNumber;
            eventWrapper.Event2 = JsonConvert.DeserializeObject<ExpandoObject>(@event, new ExpandoObjectConverter());
            return eventWrapper;
        }
    }
  
}