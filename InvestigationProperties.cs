using System.Collections.Generic;
using System.Diagnostics;

namespace Investigation
{
    internal class InvestigationProperties
    {
        public bool GenerationComplete;
        public Npc Murderer;
        public string Name;
        public List<Room> Rooms = new List<Room>();
        public List<Npc> Suspects = new List<Npc>();

        //Generate an investigation
        public InvestigationProperties()
        {
            //Generate name
            string[] FirstTitles = {"The adventure of", "A night with", "Little murder by", "The conspiration of"};
            string[] Pseudos = {"the Butcher", "the steel hands", "the shadow killer", "the gracious murderer"};

            var title = Application.Instance.Randomizer.Next(0, FirstTitles.Length);
            var pseudo = Application.Instance.Randomizer.Next(0, Pseudos.Length);
            Name = FirstTitles[title] + " " + Pseudos[pseudo];


            var NpcsNb = Application.Instance.Randomizer.Next(2, 11);
            //Generate NPCs
            for (var i = 0; i < NpcsNb; i++)
            {
                Suspects.Add(new Npc());
            }

            while (Suspects.Count < NpcsNb)
            {
            }

            GenerationComplete = true;
        }

        public void SetupFinalization()
        {
            //Select the Murderer
            var randMurderer = Application.Instance.Randomizer.Next(0, Suspects.Count);
            Murderer = Suspects[randMurderer];
            Debug.WriteLine("THE MURDERER IS : " + Murderer.Name);

            var newNpcs = new List<Npc>();
            for (var index = 0; index < Suspects.Count; index++)
            {
                var suspect = Suspects[index];
                var newSuspect = suspect;
                newSuspect.SetInfo();
                newNpcs.Add(newSuspect);
            }
            Suspects = newNpcs;

            var RoomsNb = Application.Instance.Randomizer.Next(1, 6);
            //Generate rooms
            for (var i = 0; i < RoomsNb; i++)
            {
                Rooms.Add(new Room());
            }
        }
    }
}