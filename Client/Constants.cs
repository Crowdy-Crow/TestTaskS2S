using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal static class Constants
    {
        public const string UriString = "https://localhost:44375/";
        public static readonly List<string> CommandsList = new List<string>()
        {
            "get",
            "buy"
        };
    }
}
