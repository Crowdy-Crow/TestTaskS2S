using Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Client
{
    internal class CommandsHandler
    {
        private string _command;
        public CommandsHandler(string command)
        {
            _command = command;
        }
        public async Task ExecuteCommandAsync()
        {
            try
            {
                var args = GetCommandArgs();
                var values = GetValueFromArgs(args);
                switch (GetCommandName())
                {
                    case "get":
                        string title = values.GetValueOrDefault("title");
                        var author = values.GetValueOrDefault("author");
                        var dateString = values.GetValueOrDefault("date");
                        DateTime? date = null;
                        if (dateString != null)
                        {
                            try
                            {
                                date = DateTime.Parse(dateString);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Неверный формат даты");
                                return;
                            }
                            
                        }
                        var orderBy = values.GetValueOrDefault("order-by");
                        var books = await Commands.GetBookDTOs(title, author, date, orderBy);
                        PrintBooks(books);
                        return;

                    case "buy":
                        try
                        {
                            int Id = Convert.ToInt32(values.GetValueOrDefault("Id"));
                            if (Id == 0)
                            {
                                throw new Exception("Id не введен или введен неверно");
                            }
                            var result = await Commands.BuyBook(Id);
                            Console.WriteLine(result);
                            return;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return;
                        }
                        
                        
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        private string GetCommandName()
        {
            var regex = new Regex(@"^\w*");
            MatchCollection matches = regex.Matches(_command);
            if (matches.Count > 0)
            {
                var commandName = matches[0].Value;
                if (Constants.CommandsList.Any(x => x == commandName))
                {
                    return commandName;
                }
                else
                {
                    throw new Exception("Command doesnt exist");
                }
            }
            else
            {
                throw new Exception("Command not found");
            }
        }
        private IEnumerable<Match> GetCommandArgs()
        {
            var regex = new Regex($"--\\S*=\\S*");
            IEnumerable<Match> matches = regex.Matches(_command);
            return matches;
        }
        private Dictionary<string, string> GetValueFromArgs(IEnumerable<Match> args)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (Match match in args)
            {
                var value = match.Value;
                value = value.Replace("--", "");
                value = value.Replace("\"", "");
                var keyValue = value.Split("=");
                if (keyValue.Length == 2)
                {
                    keyValue[1] = keyValue[1].Replace("\"", "");
                    keyValue[1] = keyValue[1].Replace("-", " ");
                    result.Add(keyValue[0], keyValue[1]);
                }
            }
            return result;
        }
        private void PrintBooks(IEnumerable<BookDTO> books)
        {
            if (books != null)
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book.Id + ". Title: " + book.Title + " Author: " + book.Author + " date: " + book.Date.ToString("yyyy-MM-dd"));
                }
            }
        }
    }
}

