using System;
using System.Net;
using System.Text;
using EventStore.ClientAPI;

namespace ESDemo.AppHelloWorld
{
    /// <summary>
    ///     Hello World EventStore
    ///     This program inserts a single event to the EventStore.
    ///     Then it returns the inserted event.
    ///     Don't forget to start the eventstore with `EventStore.ClusterNode.exe --db .\ESData` before running this program
    ///     (see README.md for details).
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, 1113));

            connection.ConnectAsync().Wait();

            // PART 1: Write to EventStore ------------------------
            //
            // Create new event 'myEvent'...
            var myEvent = new EventData(Guid.NewGuid(), "testEvent", false,
                Encoding.UTF8.GetBytes("some data"),
                Encoding.UTF8.GetBytes("some metadata"));

            // Append 'myEvent' to the stream 'test-stream'...
            connection.AppendToStreamAsync("test-stream", ExpectedVersion.Any, myEvent).Wait();


            // PART 2: Read from EventStore -----------------------
            //
            // Read 'test-stream'...
            var streamEvents = connection.ReadStreamEventsForwardAsync("test-stream", 0, 1, false).Result;

            // Get first event...
            var returnedEvent = streamEvents.Events[0].Event;

            Console.WriteLine("Read event with data: {0}, metadata: {1}",
                Encoding.UTF8.GetString(returnedEvent.Data),
                Encoding.UTF8.GetString(returnedEvent.Metadata));

            Console.ReadLine();
        }
    }
}