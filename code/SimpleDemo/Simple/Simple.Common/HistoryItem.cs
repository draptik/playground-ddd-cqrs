using System;

namespace Simple.Common
{
    public class HistoryItem
    {
        public string DomainEvent { get; set; }

        public DateTime TimestampUtc { get; set; }
        public string Type { get; set; }
        public int Version { get; set; }
    }
}