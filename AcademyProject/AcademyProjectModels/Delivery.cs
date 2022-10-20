using AcademyProjectModels;
using MessagePack;

namespace APIPurchaseDeliveryPublisher.Models
{
    [MessagePackObject]
    public class Delivery
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]

        public Book Book { get; set; }

        [Key(2)]

        public int Quantity { get; set; }

        public int GetKey()
        {
            return Id;
        }
    }
}
