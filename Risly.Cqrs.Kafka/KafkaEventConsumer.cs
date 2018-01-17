using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Risly.Cqrs.Kafka
{
    public class KafkaEventConsumer
    {
        /// <summary>
        /// localhost:9092
        /// </summary>
        public const string DEFAULT_BROKER_LIST = "localhost:9092";

        /// <summary>
        /// 
        /// </summary>
        public const string DEFAULT_TOPIC_NAME = "";

        public readonly IEventHandler _EventHandler;

        private bool _isStopping;

        public bool IsStopping { get { return _isStopping; } }

        public string BrokerList { get; set; }

        public string TopicName { get; set; }

        public string ConsumerGroupId { get; set; }

        /// <summary>
        /// Initializes a new instance of the Api.Events.KafkaUpdateConsumer class
        /// with specified  event handler and default broker list and topic name.
        /// </summary>
        public KafkaEventConsumer(IEventHandler EventHandler)
        {
            BrokerList = DEFAULT_BROKER_LIST;
            TopicName = DEFAULT_TOPIC_NAME;

            _EventHandler = EventHandler;
        }

        public void Start()
        {
            // TODO: Load configuration from file instead of statically in code
            var config = new Dictionary<string, object>
            {
                { "group.id", ConsumerGroupId },
                { "enable.auto.commit", false },
                { "bootstrap.servers", BrokerList },
                { "default.topic.config", new Dictionary<string, object>()
                    {
                        { "auto.offset.reset", "smallest" }
                    }
                }
            };

            using (var consumer = new Consumer<Ignore, Event>(config, null, new EventDeserializer()))
            {
                // TODO: Properly handle error
                consumer.OnError += (_, error)
                    => Console.WriteLine($"Error: {error}");

                // TODO: Properly handle consume error
                consumer.OnConsumeError += (_, error)
                    => Console.WriteLine($"Consume error: {error}");

                consumer.Subscribe(TopicName);
                
                while(!IsStopping)
                {
                    try
                    {
                        Message<Ignore, Event> message;
                        if (!consumer.Consume(out message, TimeSpan.FromMilliseconds(100)))
                        {
                            continue;
                        }

                        _EventHandler.Handle(message.Value);

                        var committedOffsets = consumer.CommitAsync(message).Result;
                    }
                    catch(Exception ex)
                    {
                        // TODO: Properly handle exception
                        Console.WriteLine($"ERROR: {ex.Message}");
                    }
                }
            }
        }

        public void Stop()
        {
            _isStopping = true;
        }
    }
}