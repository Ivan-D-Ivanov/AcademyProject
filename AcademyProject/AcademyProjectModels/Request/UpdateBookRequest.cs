namespace AcademyProjectModels.Request
{
    public class UpdateBookRequest
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int AuthorId { get; init; }
    }
}
