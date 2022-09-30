using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyProjectModels
{
    public record Author
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public int Age { get; init; }

        public DateTime DateOfBirth { get; init; }

        public string NickName { get; init; }
    }
}
