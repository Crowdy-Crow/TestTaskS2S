using System;

namespace API.Data.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// TODO: Спросить год это будет или дата
        /// </summary>
        public DateTime Date { get; set; }

    }
}
