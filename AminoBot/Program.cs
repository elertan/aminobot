using System;

namespace AminoBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.Run().Wait();
        }
    }
}