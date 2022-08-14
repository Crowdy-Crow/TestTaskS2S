using API.Data.Models;
using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using API.Data.Repositories;

namespace API.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;

        }
        public bool BuyBook(int Id)
        {
            var book = _bookRepository.GetBookByID(Id);
            if (book != null)
            {
                return _bookRepository.DeleteBook(book);
            }
            else return false;
        }

        public IEnumerable<BookDTO> GetBooks(
            string tittle = null,
            string author = null,
            DateTime? date = null,
            string orderBy = null
            )
        {
            var booksFromDB = _bookRepository.GetBooks();
            if (tittle != null)
            {
                booksFromDB = booksFromDB.Where(x => x.Title == tittle);
            }
            if (author != null)
            {
                booksFromDB = booksFromDB.Where(x => x.Author == author);
            }
            if (date != null)
            {
                booksFromDB = booksFromDB.Where(x => x.Date == date);
            }

            var bookDTOs = new List<BookDTO>();
            foreach (var book in booksFromDB)
            {
                var bookDTO = _mapper.Map<BookDTO>(book);
                bookDTOs.Add(bookDTO);
            }

            if (orderBy != null)
            {
                return bookDTOs.OrderBy(x => x.GetType().GetProperties().FirstOrDefault(x => x.Name.ToLower() == orderBy.ToLower()).GetValue(x, null));
            }
            return bookDTOs;
        }
    }
}
