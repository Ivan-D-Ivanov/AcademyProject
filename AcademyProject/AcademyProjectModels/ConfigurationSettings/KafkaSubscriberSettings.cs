namespace AcademyProjectModels.CongigurationSettings
{
    public class KafkaSubscriberSettings
    {
        public string BootstrapServers { get; set; }

        public int AutoOffsetReset { get; set; }

        public string GroupId { get; set; }
    }
}
