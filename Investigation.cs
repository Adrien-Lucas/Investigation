namespace Investigation
{
    internal class Investigation
    {
        //This class handle the current investigation
        //It shows the Main screen and control the access to the other screens

        //An instance of investigation properties
        public InvestigationProperties Properties;

        //Show the main screen that contains the choice of talking, searching or suspecting
        public void ShowMainScreen()
        {
            Program.WriteScreen(
                "Someone has been assassinated tonight, " + Properties.Suspects.Count + " suspects have been arrested" +
                "/nand some places rely to the investigation", 25, Properties.Name, true);

            var choice = Application.Instance.DoAChoice("What do you want to do ?",
                new[] {"Talk", "Search", "Name the murderer"});

            switch (choice)
            {
                case 1:
                    ShowNpcScreen();
                    break;
                case 2:
                    ShowSearchScreen();
                    break;
                case 3:
                    var suspicion = new Suspicion();
                    suspicion.ShowSuspicionScreen();
                    break;
            }
        }

        //Shows a list of suspects, you can choose who you wan't to interrogate
        public void ShowNpcScreen()
        {
            Program.WriteScreen("In this room near the murder place you see all the suspects", 25,
                Properties.Name + " - Talk", true);

            //Generate an array of suspects names
            var names = new string[Properties.Suspects.Count + 1];
            var i = 0;
            foreach (var suspect in Properties.Suspects)
            {
                names[i] = suspect.Name;
                i++;
            }
            names[Properties.Suspects.Count] = "Get back"; //Add get back to choices
            //--

            var selected = Application.Instance.DoAChoice("Who do you want to interrogate ?", names);
            if (selected != Properties.Suspects.Count + 1)
                Properties.Suspects[selected - 1].Interrogate();
            else
                ShowMainScreen();
        }

        //Shows a list of searchable places, you can choose what place you want to search
        public void ShowSearchScreen()
        {
            Program.WriteScreen("A few possibilities are given to you", 25, "Search", true);

            //Generate an array of rooms names
            var names = new string[Properties.Rooms.Count + 1];
            var i = 0;
            foreach (var room in Properties.Rooms)
            {
                names[i] = room.Name;
                i++;
            }
            names[Properties.Rooms.Count] = "Get back"; //Add get back to choices
            //--

            var choice = Application.Instance.DoAChoice("What place do you want to search ?", names);
            if (choice != Properties.Rooms.Count + 1)
                Properties.Rooms[choice - 1].Search();
            else
                ShowMainScreen();
        }
    }
}