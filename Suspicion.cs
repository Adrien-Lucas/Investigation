namespace Investigation
{
    //This class shows the final screen where the player has to give his
    //verdict, it also determine if the has win or lose and show the end of 
    //game screen

    internal class Suspicion
    {
        //Shows the screen
        public void ShowSuspicionScreen()
        {
            Program.WriteScreen("You are going to give your verdict, you won't have" +
                                "/nany second chance ...",
                                50, "Suspicion", true);

            //Get suspects names
            var names = new string[Application.Instance.Investigation.Properties.Suspects.Count];
            var i = 0;
            foreach (var suspect in Application.Instance.Investigation.Properties.Suspects)
            {
                names[i] = suspect.Name;
                i++;
            }
            //--

            //Wait for verdict
            int verdict = Application.Instance.DoAChoice("Who is the murderer ?", names) - 1;
            string verdictName = Application.Instance.Investigation.Properties.Suspects[verdict].Name;
            Program.WriteScreen(verdictName + " is prisoned after your intervention, waiting for the trial", 25);
            Program.WriteScreen("Month later it's prove that you were", 50);
            Program.WriteScreen("...", 1000); //long delay to get some suspens !

            //Win
            if (verdictName == Application.Instance.Investigation.Properties.Murderer.Name)
                Program.WriteScreen("Right ! " + verdictName + " is going to be hang in few days", 100);
            //Lose
            else
                Program.WriteScreen("False ..." + verdictName + " is free and you reputation decreased/n", 100);

            Program.WaitKeyToContinue();

            //End of game screen, Restart / Exit ?
            Program.WriteScreen("Thank you for playing Investigation !", 25, "END", true);
            var choice = Application.Instance.DoAChoice("What do you want to do ?", new[] {"Restart", "Exit"});

            if (choice == 1)
                Application.Instance.Run(); //Restart the game
        }
    }
}