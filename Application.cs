using System;

namespace Investigation
{
    internal class Application
    {
        public static Application Instance;
        private Investigation _investigation;
        public Random Randomizer = new Random();

        static Application()
        {
            Instance = new Application();
        }

        public Investigation Investigation => _investigation ?? (_investigation = new Investigation());

        public void Run()
        {
            Console.Title = "Investigation - MrBrenan 2016";
            Console.SetWindowSize(75, 25);

            Program.WriteScreen(
                "Investigation is a game where you have to find the murderer by/ntalking to people and visit places. /nGood Luck !",
                25, "INVESTIGATION", true);

            Program.WaitKeyToContinue();
            Investigation.Properties = new InvestigationProperties();
            Investigation.Properties.SetupFinalization();
            Investigation.ShowMainScreen();
        }

        public int DoAChoice(string question, string[] choices)
        {
            Console.WriteLine(" ");
            Program.WriteScreen(question, 15);
            Console.WriteLine(" ");

            var i = 0;
            foreach (var choice in choices)
            {
                i++;
                Program.WriteScreen(i + ". " + choice, 15);
            }

            Console.WriteLine("");
            Program.WriteScreen("Answer : ", 50);

            var answer = Console.ReadLine();
            var parsedResult = int.Parse(answer);
            if (parsedResult < i + 1)
                return parsedResult;
            DoAChoice(question, choices);

            return 0;
        }
    }
}