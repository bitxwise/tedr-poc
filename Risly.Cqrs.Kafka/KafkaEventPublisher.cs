/* Ensure Zookeeper and Kafka are running for this publisher to work
 * 
 * In two command prompts:
 * 
 * C1> zkserver
 * C2> cd C:\Apache\kafka_2.12-1.0.0
 * C2> .\bin\windows\kafka-server-start.bat .\config\server.properties
 */

using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Risly.Cqrs;

namespace Risly.Cqrs.Kafka
{
    public class KafkaEventPublisher : IEventPublisher
    {
        /// <summary>
        /// localhost:9092
        /// </summary>
        public const string DEFAULT_BROKER_LIST = "localhost:9092";

        /// <summary>
        /// 
        /// </summary>
        public const string DEFAULT_TOPIC_NAME = "";

        public string BrokerList { get; set; }

        public string TopicName { get; set; }

        /// <summary>
        /// Initializes a new instance of the Api.Events.KafkaEventPublisher class
        /// with default broker list and topic name.
        /// </summary>
        public KafkaEventPublisher()
        {
            BrokerList = DEFAULT_BROKER_LIST;
            TopicName = DEFAULT_TOPIC_NAME;
        }

        public void Publish<T>(T @event) where T : Event
        {
            try
            {               
                // TODO: Load configuration from file instead of statically in code
                var config = new Dictionary<string, object> { { "bootstrap.servers", BrokerList }};

                // TODO: Consider having a single instance of producer associated with the instance of this class
                using (var producer = new Producer<Null, Event>(config, null, new EventSerializer()))
                {
                    var deliveryReport = producer.ProduceAsync(TopicName, null, @event);
                    deliveryReport.ContinueWith(task =>
                    {
                        Console.WriteLine($"Partition: {task.Result.Partition}, Offset: {task.Result.Offset}");
                    });
                    
                    // Tasks are not waited on synchronously (ContinueWith is not synchronous),
                    // so it's possible they may still in progress here.
                    producer.Flush(TimeSpan.FromSeconds(10));
                }
            }
            catch(Exception ex)
            {
                // TODO: Properly handle publish error
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}