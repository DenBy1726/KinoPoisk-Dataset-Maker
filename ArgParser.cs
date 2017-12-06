using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskParser
{
    /// <summary>
    /// Формирует словарь из именованных аргументов и множество из флагов
    /// </summary>
    public class ArgParser
    {
        private Dictionary<string, string> arg = new Dictionary<string, string>();
        private HashSet<string> key = new HashSet<string>();
        public ArgParser(string[] args)
        {
            foreach(var it in args)
            {
                var kvPair = it.Split('=').Select(x=>x.ToLower()).ToArray();
                if(kvPair.Length == 2)
                {
                    try
                    {
                        arg[kvPair[0]] = kvPair[1];
                    }
                    catch
                    {
                        Console.WriteLine($"Each argument should be unique " +
                            $"(Argument {kvPair[0]} already exist).");
                    }
                }
                else if(kvPair.Length == 0)
                {
                    try
                    {
                        key.Add(it);
                    }
                    catch
                    {
                        Console.WriteLine($"Each key should be unique " +
                            $"(Key {it} already exist).");
                    }
                }
            }
        }

        public Dictionary<string, string> Arg { get => arg; set => arg = value; }
        public HashSet<string> Key { get => key; set => key = value; }
    }
}
