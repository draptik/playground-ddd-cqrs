using System;

namespace Simple.Eventstore
{
    public static class StreamNameExtensions
    {
        private static char _separator = '@';

        public static Guid Id(this string streamName)
        {
            var strings = streamName.Split(_separator);
            return new Guid(strings[1]);
        }

        public static string Type(this string streamName)
        {
            var strings = streamName.Split(_separator);
            return strings[0];
        }
    }
}