namespace AcademyProjectModels
{
    public record Book
    {
        public int Id { get; init; }

        public string? Title { get; init; }

        public int AuthorId { get; init; }
    }
}
