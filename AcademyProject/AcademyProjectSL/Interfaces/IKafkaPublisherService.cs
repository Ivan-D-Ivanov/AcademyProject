namespace AcademyProjectSL.Interfaces
{
    public interface IKafkaPublisherService<Tkey, TValue>
    {
        Task PublishTopic(Tkey key, TValue person);
    }
}
