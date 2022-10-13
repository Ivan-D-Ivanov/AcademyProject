using Confluent.Kafka;
using MessagePack;

namespace AcademyProjectModels.KafkaModels
{
    public class MessagePackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T date, SerializationContext context)
        {
            return MessagePackSerializer.Serialize(date);
        }
    }
}
