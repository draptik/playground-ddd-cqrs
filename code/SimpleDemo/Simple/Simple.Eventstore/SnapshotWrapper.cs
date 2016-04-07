using System;

namespace Simple.Eventstore
{
    public class SnapshotWrapper
    {
        public string StreamName { get; set; }

        public Object Snapshot { get; set; }

        public DateTime Created { get; set; }
    }
}