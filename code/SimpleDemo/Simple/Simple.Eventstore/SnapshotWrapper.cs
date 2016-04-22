using System;

namespace Simple.Eventstore
{
    public class SnapshotWrapper
    {
        public SnapshotWrapper()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public string StreamName { get; set; }

        public object Snapshot { get; set; }

        public DateTime Created { get; set; }
    }
}