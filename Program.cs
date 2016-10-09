using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace Investigation
{
    internal class Program
    {
        //This void is the Program void called when the program launch
        private static void Main()
        {
            Application.Instance.Run();
        }

        public static void WriteScreen(string text, int delay)
        {
            var lines = Regex.Split(text, "/n");
            foreach (var line in lines)
            {
                foreach (var character in line)
                {
                    Console.Write(character);
                    Thread.Sleep(delay);
                }
                Console.WriteLine("");
            }
        }

        public static void WriteScreen(string text, int delay, string title, bool fancy)
        {
            Console.Clear();
            if (fancy)
                Console.WriteLine("==========================================================================");

            //Centered title
            var pad = "";
            for (var i = 0; i < (74 - title.Length)/2; i++)
            {
                pad += " ";
            }
            Console.WriteLine(pad + title);

            if (fancy)
                Console.WriteLine("==========================================================================");
            Console.WriteLine("");
            WriteScreen(text, delay);
            //Console.ReadLine();
        }

        public static void WaitKeyToContinue()
        {
            Console.WriteLine("");
            Console.Write("Press a key to continue ...");
            Console.ReadKey();
        }
    }
}