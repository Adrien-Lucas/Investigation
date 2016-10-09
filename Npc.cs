namespace Investigation
{
    internal class Npc
    {
        private string Info = "";
        public float Intimidate = Application.Instance.Randomizer.Next(0, 100);
        public bool Male;
        public string Name;
        public float Persuade = Application.Instance.Randomizer.Next(0, 100);
        public string Portrait;
        public bool ToldInfo;

        public Npc()
        {
            Male = Application.Instance.Randomizer.Next(0, 2) == 0;

            string[] FirstName;
            string[] LastName = {"Ellis", "Hamilton", "Walker", "Stewart", "Grimes", "Lloyd"};
            string[] Adjectives;

            if (Male)
            {
                FirstName = new[] {"Tommy", "Dexter", "Ryan", "Frederick", "Cesar", "Arthur"};
                Adjectives = new[] {"handsome", "stocky", "nervous", "ugly", "noble", "redneck"};
            }
            else
            {
                FirstName = new[] {"Eloise", "Harriet", "Eva", "Caroline", "Abigail", "Lucy"};
                Adjectives = new[] {"beautiful", "stocky", "nervous", "ugly", "noble", "redneck"};
            }


            var first = Application.Instance.Randomizer.Next(0, FirstName.Length);
            var last = Application.Instance.Randomizer.Next(0, LastName.Length);
            var portrait = Application.Instance.Randomizer.Next(0, Adjectives.Length);
            Name = FirstName[first] + " " + LastName[last];
            Portrait = Name + " is a " + Adjectives[portrait] + " " + (Male ? "men" : "women") + "/n" +
                       (Male ? "He" : "She") + " receives you";
        }

        public void SetInfo()
        {
            var truthness = Application.Instance.Randomizer.Next(0, 100);
            if (truthness <= 50 && Name != Application.Instance.Investigation.Properties.Murderer.Name)
                Info = "I suspect " + Application.Instance.Investigation.Properties.Murderer.Name +
                       " to be the murderer";
            else
            {
                var otherNpcs = Application.Instance.Investigation.Properties.Suspects;
                otherNpcs.Remove(this);
                var rand = Application.Instance.Randomizer.Next(0, otherNpcs.Count);
                Info = "I suspect " + otherNpcs[rand].Name + " to be the murderer";
            }
        }

        public void Interrogate()
        {
            Program.WriteScreen(Portrait, 25, "Interrogate " + Name, true);

            if (!ToldInfo)
            {
                var choice = Application.Instance.DoAChoice("What do you want to ask ?",
                    new[] {"Intimidate", "Persuade", "Simply ask", "Get back"});
                var result = Application.Instance.Randomizer.Next(0, 100);

                Program.WriteScreen("", 0, "Answer", true);

                switch (choice)
                {
                    case 1:
                        if (result <= Intimidate)
                        {
                            Program.WriteScreen("Okay I'll tell you all I know but please don't hurt me !", 10);
                            TellInfo();
                        }
                        else
                            Program.WriteScreen("You'r a brute ! I won't tell you anything !", 10);

                        Intimidate = -1;
                        Program.WaitKeyToContinue();
                        break;
                    case 2:
                        if (result <= Persuade)
                        {
                            Program.WriteScreen("You'r right, I'll tell you what I know", 25);
                            TellInfo();
                        }
                        else
                            Program.WriteScreen("Sorry but I didn't agree with you point of view", 10);

                        Persuade = -1;
                        Program.WaitKeyToContinue();
                        break;
                    case 3:
                        if (result <= 25)
                        {
                            Program.WriteScreen("It's so nicely ask", 25);
                            TellInfo();
                        }
                        else
                            Program.WriteScreen("I don't know anything about this case", 10);

                        Program.WaitKeyToContinue();
                        break;
                }
                if (ToldInfo || choice == 4)
                    Application.Instance.Investigation.ShowNpcScreen();
                else
                    Interrogate();
            }
            else
            {
                Program.WriteScreen("I've already told you all I know/n" + Info, 25);
                Program.WaitKeyToContinue();
                Application.Instance.Investigation.ShowNpcScreen();
            }
        }

        public void TellInfo()
        {
            Program.WriteScreen(Info, 50);
            ToldInfo = true;
        }
    }
}