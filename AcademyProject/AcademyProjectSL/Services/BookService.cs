using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyProjectDL.Repositories.InMemoryRepo;
using AcademyProjectModels;
using AcademyProjectSL.Interfaces;

namespace AcademyProjectSL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookInMemoryRepo _bookInMemoryRepo;

        public BookService(IBookInMemoryRepo bookInMemoryRepo)
        {
            _bookInMemoryRepo = bookInMemoryRepo;
        }

        public IEnumerable<Book> GetAllBooks => _bookInMemoryRepo.GetAllBooks;

        public Book? AddBook(Book book)
        {
            _bookInMemoryRepo.AddBook(book);
            return book;
        }

        public Book? DeleteBook(int bookId)
        {
            return _bookInMemoryRepo.DeleteBook(bookId);
        }

        public Book? GetById(int id)
        {
            return _bookInMemoryRepo.GetById(id);
        }

        public Guid GetId()
        {
            return _bookInMemoryRepo.GetId();
        }

        public Book? UpdateBook(Book book)
        {
            return _bookInMemoryRepo.UpdateBook(book);
        }
    }
}
