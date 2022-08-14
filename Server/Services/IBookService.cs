using API.Data.Models;
using Contracts;
using System;
using System.Collections.Generic;

namespace API.Services
{
    public interface IBookService
    {
        public IEnumerable<BookDTO> GetBooks(
            string tittle = null,
            string author = null,
            DateTime? date = null,
            string orderBy = null);
        public bool BuyBook(int Id);
    }
}
