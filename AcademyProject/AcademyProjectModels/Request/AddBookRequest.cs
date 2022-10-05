namespace AcademyProjectModels.Request
{
    public class AddBookRequest
    {
        public string Title { get; init; }

        public int AuthorId { get; init; }

        public int Quantity { get; init; }

        public decimal Price { get; init; }
    }
}
