namespace Simple.Common
{
    public class EventStream
    {
        private EventStream()
        {
        }

        public EventStream(string id)
        {
            Id = id;
            Version = 0;
        }

        public string Id { get; } //aggregate type + id
        public int Version { get; private set; }

        public EventWrapper RegisterEvent(DomainEvent @event)
        {
            Version++;

            return new EventWrapper(@event, Version, Id);
        }
    }
}