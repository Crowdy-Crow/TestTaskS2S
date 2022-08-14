using Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace Client
{
    internal class Program
    {

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("Write a command");
                var command = Console.ReadLine();

                CommandsHandler commandsHandler = new CommandsHandler(command);
                commandsHandler.ExecuteCommandAsync();
            }
        }

    }
}
