using System;


namespace Investigation
{
    //This class is the main class that support the game
    //It contains an instance that is later called for in game actions
    //Application is run by Program.cs

    internal class Application
    {
        //This is the instance that will contains all values of the current game
        public static Application Instance;
        //An instance of Investigation.cs that contains the main screen and generate properties of the current investigation
        private Investigation _investigation;
        //A randomizer used many times to generate numbers in other class
        public Random Randomizer = new Random();

        //Constructor that set the instance
        static Application()
        {
            Instance = new Application();
        }

        //Investigation property, when get generate if null
        public Investigation Investigation => _investigation ?? (_investigation = new Investigation());

        //Main void for running the program, windows settings and first screen
        public void Run()
        {
            //Set console properties
            Console.Title = "Investigation - MrBrenan 2016";
            Console.SetWindowSize(75, 25);

            Program.WriteScreen(
                "Investigation is a game where you have to find the murderer by/ntalking to people and visit places. /nGood Luck !",
                25, "INVESTIGATION", true);

            Program.WaitKeyToContinue();
            //Generate investigation properties
            Investigation.Properties = new InvestigationProperties();
            Investigation.Properties.SetupFinalization();
            //Exit first screen to show the main screen
            Investigation.ShowMainScreen();
        }

        //Very usefull method to give a choice to the player
        // Question ?
        //
        // 1. Choice 1
        // 2. Choice 2
        // x. Choice x
        //
        // Answer ?
        // (Input)
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

            //wait for player answer
            var answer = Console.ReadLine();
            var parsedResult = int.Parse(answer);
            if (parsedResult < i + 1)
                return parsedResult;
            DoAChoice(question, choices);

            return 0;

            //Should add an input verification
        }
    }
}