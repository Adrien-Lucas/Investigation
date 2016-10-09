namespace Investigation
{
    //This class contain all the infos about an npc
    //They are plenty instance of this class

    internal class Npc
    {
        private string _info = ""; //know infos by the npc
        public float Intimidate = Application.Instance.Randomizer.Next(0, 100); //Generate an intimidation score
        public bool Male; //Sexe of the npc
        public string Name; //Name of the npc
        public float Persuade = Application.Instance.Randomizer.Next(0, 100); //Generate a persuade score
        public string Portrait; //A simple portrait of the npc
        public bool ToldInfo; //Has he already spoke everything ?

        //Constructor that generate the npc
        public Npc()
        {
            //Random sexe
            Male = Application.Instance.Randomizer.Next(0, 2) == 0;

            //Name and portrait generation --
            string[] firstName;
            string[] lastName = {"Ellis", "Hamilton", "Walker", "Stewart", "Grimes", "Lloyd"};
            string[] adjectives;

            if (Male) //Names and adjectives depend of the sexe
            {
                firstName = new[] {"Tommy", "Dexter", "Ryan", "Frederick", "Cesar", "Arthur"};
                adjectives = new[] {"handsome", "stocky", "nervous", "ugly", "noble", "redneck"};
            }
            else
            {
                firstName = new[] {"Eloise", "Harriet", "Eva", "Caroline", "Abigail", "Lucy"};
                adjectives = new[] {"beautiful", "stocky", "nervous", "ugly", "noble", "redneck"};
            }


            var first = Application.Instance.Randomizer.Next(0, firstName.Length);
            var last = Application.Instance.Randomizer.Next(0, lastName.Length);
            var portrait = Application.Instance.Randomizer.Next(0, adjectives.Length);
            Name = firstName[first] + " " + lastName[last];
            Portrait = Name + " is a " + adjectives[portrait] + " " + (Male ? "men" : "women") + "/n" +
                       (Male ? "He" : "She") + " receives you";
            //--
        }

        //Set the var _info     Choose if the npc will tell the truth of not
        public void SetInfo()
        {
            //Generate a truth score that will say if he sais truth or not
            var truthness = Application.Instance.Randomizer.Next(0, 100);
            //He says truth and is not the murderer
            if (truthness <= 50 && Name != Application.Instance.Investigation.Properties.Murderer.Name)
                _info = "I suspect " + Application.Instance.Investigation.Properties.Murderer.Name +
                       " to be the murderer";
            else //He tells you bullshit
            {
                var otherNpcs = Application.Instance.Investigation.Properties.Suspects; //list of suspect without actual npc
                otherNpcs.Remove(this);
                var rand = Application.Instance.Randomizer.Next(0, otherNpcs.Count);
                _info = "I suspect " + otherNpcs[rand].Name + " to be the murderer";
            }
        }

        //Shows interrogation screen for this npc
        public void Interrogate()
        {
            Program.WriteScreen(Portrait, 25, "Interrogate " + Name, true);

            if (!ToldInfo) //Only if hasn't spoke already
            {
                var choice = Application.Instance.DoAChoice("What do you want to ask ?",
                    new[] {"Intimidate", "Persuade", "Simply ask", "Get back"});
                var result = Application.Instance.Randomizer.Next(0, 100); //Generate a score that would determine chances of getting infos

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
            else //If already told everything 
            {
                Program.WriteScreen("I've already told you all I know/n" + _info, 25);
                Program.WaitKeyToContinue();
                Application.Instance.Investigation.ShowNpcScreen();
            }
        }

        //A simple shortcut
        public void TellInfo()
        {
            Program.WriteScreen(_info, 50);
            ToldInfo = true;
        }
    }
}