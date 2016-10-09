using System.Collections.Generic;

namespace Investigation
{
    internal class Investigation
    {
        public InvestigationProperties Properties;

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

        public void ShowNpcScreen()
        {
            Program.WriteScreen("In this room near the murder place you see all the suspects", 25,
                Properties.Name + " - Talk", true);

            var names = new string[Properties.Suspects.Count + 1];
            var i = 0;
            foreach (var suspect in Properties.Suspects)
            {
                names[i] = suspect.Name;
                i++;
            }
            names[Properties.Suspects.Count] = "Get back";

            var selected = Application.Instance.DoAChoice("Who do you want to interrogate ?", names);
            if (selected != Properties.Suspects.Count + 1)
                Properties.Suspects[selected - 1].Interrogate();
            else
                ShowMainScreen();
        }

        public void ShowSearchScreen()
        {
            Program.WriteScreen("A few possibilities are given to you", 25, "Search", true);

            var names = new string[Properties.Rooms.Count + 1];
            var i = 0;
            foreach (var room in Properties.Rooms)
            {
                names[i] = room.Name;
                i++;
            }
            names[Properties.Rooms.Count] = "Get back";

            var choice = Application.Instance.DoAChoice("What place do you want to search ?", names);
            if (choice != Properties.Rooms.Count + 1)
                Properties.Rooms[choice - 1].Search();
            else
                ShowMainScreen();
        }
    }
}