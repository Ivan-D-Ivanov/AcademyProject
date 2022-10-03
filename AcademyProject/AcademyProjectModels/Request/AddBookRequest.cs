namespace AcademyProjectModels.Request
{
    public class AddBookRequest
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public int AuthorId { get; init; }
    }
}
