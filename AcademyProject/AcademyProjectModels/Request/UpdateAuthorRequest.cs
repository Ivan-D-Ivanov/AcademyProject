namespace AcademyProjectModels.Request
{
    public class UpdateAuthorRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime Date { get; set; }

        public string NickName { get; set; }
    }
}
