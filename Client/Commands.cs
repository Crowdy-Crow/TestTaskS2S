using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    static internal class Commands
    {
        internal static async Task <IEnumerable<BookDTO>> GetBookDTOs(
            string title,
            string author,
            DateTime? date,
            string orderBy
            )
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    IEnumerable<BookDTO> bookDTOs;
                    var responce = await httpClient.GetAsync(Constants.UriString + $"Books?title={title}&author={author}&date={date}&orderBy={orderBy}");
                    var content = await responce.Content.ReadAsStringAsync();
                    bookDTOs = JsonConvert.DeserializeObject<List<BookDTO>>(content);
                    if (bookDTOs.Count() == 0)
                    {
                        Console.WriteLine("Books not found");
                    }
                    return bookDTOs;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        internal static async Task <bool> BuyBook(int Id)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var responce = await httpClient.PostAsync(Constants.UriString + $"Books?Id=" + Id, null);
                    var content = await responce.Content.ReadAsStringAsync();
                    return Convert.ToBoolean(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
