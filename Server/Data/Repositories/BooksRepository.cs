using API.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.Data.Repositories
{
    public class BooksRepository : IBookRepository
    {
        private readonly AppDbContext _appDbContext;
        public BooksRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool DeleteBook(Book book)
        {
            if (book == null)
            {
                return false;
            }
            _appDbContext.books.Remove(book);
            _appDbContext.SaveChanges();
            return true;
        }

        public Book GetBookByID(int Id)
        {
            var book = _appDbContext.books.FirstOrDefault(x => x.Id == Id);
            return book;
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = _appDbContext.books.ToList();
            return books;
        }
    }
}
