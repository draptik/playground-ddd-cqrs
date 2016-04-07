namespace Simple.Common
{
    public class EventWrapper
    {
        public string Id { get; }
        public DomainEvent Event { get; }
        public string EventStreamId { get; }
        public int EventNumber { get; }

        public EventWrapper(DomainEvent @event, int eventNumber, string streamStateId)
        {
            Event = @event;
            EventNumber = eventNumber;
            EventStreamId = streamStateId;
            Id = $"{streamStateId}-{EventNumber}";
        }
    }
}