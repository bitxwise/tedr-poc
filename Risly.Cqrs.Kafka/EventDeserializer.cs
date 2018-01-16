using System.Collections.Generic;
using System.Text;
using Confluent.Kafka.Serialization;
using Newtonsoft.Json;
using Risly.Cqrs;

namespace Risly.Cqrs.Kafka
{
    public class EventDeserializer : IDeserializer<Event>
    {
        JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public IEnumerable<KeyValuePair<string, object>> Configure(IEnumerable<KeyValuePair<string, object>> config, bool isKey)
        {
            // nothing to configure
            return config;
        }

        public Event Deserialize(string topic, byte[] data)
        {
            return JsonConvert.DeserializeObject<Event>(Encoding.Default.GetString(data), _settings);
        }
    }
}