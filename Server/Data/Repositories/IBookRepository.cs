using API.Data.Models;
using System.Collections.Generic;

namespace API.Data.Repositories
{
    public interface IBookRepository
    {
        public Book GetBookByID(int Id);
        public IEnumerable<Book> GetBooks();
        public bool DeleteBook(Book book);
    }
}
