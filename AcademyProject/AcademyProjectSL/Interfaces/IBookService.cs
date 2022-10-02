using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyProjectModels;

namespace AcademyProjectSL.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks { get; }
        Book? AddBook(Book book);
        Book? DeleteBook(int bookId);
        Book? GetById(int id);
        Book? UpdateBook(Book book);
        Guid GetId();
    }
}
