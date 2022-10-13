using MessagePack;

namespace AcademyProjectModels
{
    [MessagePackObject]
    public class Person
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public string Name { get; set; }
        [Key(2)]
        public int Age { get; set; }
    }
}