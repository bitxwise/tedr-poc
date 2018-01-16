using System.Collections.Generic;
using System.Text;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;

namespace Risly.Cqrs.Kafka
{
    public class EventSerializer : ISerializer<Event>
    {
        JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public IEnumerable<KeyValuePair<string, object>> Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
        {
            // nothing to configure
            return config;
        }

        public byte[] Serialize(string topic, Event data)
        {
            return Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data, _settings));
        }
    }
}