using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskParser
{
    public static class Selectors
    {
        public const string Author = "div:nth-child(2) > div:nth-child(2) > p:nth-child(1) > a:nth-child(2)";
        public const string Film = "div:nth-child(2) > div:nth-child(2) > p:nth-child(2) > a:nth-child(1) > span:nth-child(1)";
        public const string Title = "div:nth-child(2) > div:nth-child(2) > p:nth-child(3)";
        public const string Date = "span:nth-child(8)";
        public const string Text = "table:nth-child(3) > tbody:nth-child(1) > tr:nth-child(1) > td:nth-child(1) > div:nth-child(1)";
    }
}
