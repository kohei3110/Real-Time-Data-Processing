﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
namespace Sender
{
    class Program
    {
        private static string json = File.ReadAllText("C:/Users/koheisaito/OneDrive - Microsoft/Documents/my/demos/Real-Time-Data-Processing/data/sample.json");

        // connection string to the Event Hubs namespace
        private const string connectionString = "Endpoint=sb://real-time-data-streaming.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=z2cIudhUVtJB9mBfrnNcJI4BJZ+mc9leSeWlzCzkv2Y=";

        // name of the event hub
        // private const string eventHubName = "eventhub01";
        private const string eventHubName = "eventhub02";

        // number of events to be sent to the event hub
        private const int numOfEvents = 3;

        static EventHubProducerClient producerClient;

        static async Task Main()
        {
            // Create a producer client that you can use to send events to an event hub
            producerClient = new EventHubProducerClient(connectionString, eventHubName);

            // Create a batch of events 
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            for (int i = 1; i <= numOfEvents; i++)
            {
                // if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes($"Event {i}"))))
                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(json))))
                {
                    // if it is too large for the batch
                    throw new Exception($"Event {i} is too large for the batch and cannot be sent.");
                }
            }

            try
            {
                // Use the producer client to send the batch of events to the event hub
                await producerClient.SendAsync(eventBatch);
                Console.WriteLine(eventBatch);
                Console.WriteLine($"A batch of {numOfEvents} events has been published.");
            }
            finally
            {
                await producerClient.DisposeAsync();
            }
        }
    }
}
