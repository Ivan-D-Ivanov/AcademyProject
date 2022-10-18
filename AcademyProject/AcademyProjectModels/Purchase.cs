namespace AcademyProjectModels
{
    public record Purchase
    {
        public Guid Id { get; set; }

        public IList<Book> Books { get; set; }

        public decimal TotalMoney { get; set; }

        public int UserId { get; set; }
    }
}
