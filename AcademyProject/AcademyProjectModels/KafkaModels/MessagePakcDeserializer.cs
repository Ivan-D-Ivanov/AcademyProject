using Confluent.Kafka;
using MessagePack;

namespace AcademyProjectModels.KafkaModels
{
    public class MessagePakcDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            return MessagePackSerializer.Deserialize<T>(data.ToArray());
        }
    }
}
