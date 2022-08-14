using API.Services;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IEnumerable <BookDTO> GetBooks(
            string title,
            string author,
            DateTime? date,
            string orderBy)
        {
            var books = _bookService.GetBooks(title, author, date, orderBy);
            return books;
        }

        [HttpPost]
        public bool BuyBook(int Id)
        {
            var result = _bookService.BuyBook(Id);
            return result;
        }
    }
}
