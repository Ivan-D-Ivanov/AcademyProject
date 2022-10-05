namespace AcademyProjectModels.Request
{
    public class UpdateAuthorRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string NickName { get; set; }
    }
}
