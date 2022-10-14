using MessagePack;

namespace AcademyProjectModels
{
    [MessagePackObject]
    public record Book
    {
        [Key(0)]
        public int Id { get; init; }

        [Key(1)]
        public string Title { get; init; }

        [Key(2)]
        public int AuthorId { get; init; }

        [Key(3)]
        public int Quantity { get; set; }

        [Key(4)]
        public DateTime LastUpdated { get; set; }

        [Key(5)]
        public decimal Price { get; set; }
    }
}
