using System.Collections.Generic;
using System.Diagnostics;

namespace Investigation
{
    //This class generate and contains every informations about the investigation
    //It generate the name of the investigation, the npcs, the rooms and choose a random murderer

    internal class InvestigationProperties
    {
        public Npc Murderer;
        public string Name;
        public List<Room> Rooms = new List<Room>();
        public List<Npc> Suspects = new List<Npc>();

        //Generate an investigation
        public InvestigationProperties()
        {
            //Generate name
            string[] firstTitles = {"The adventure of", "A night with", "Little murder by", "The conspiration of"};
            string[] pseudos = {"the Butcher", "the steel hands", "the shadow killer", "the gracious murderer"};

            var title = Application.Instance.Randomizer.Next(0, firstTitles.Length);
            var pseudo = Application.Instance.Randomizer.Next(0, pseudos.Length);
            Name = firstTitles[title] + " " + pseudos[pseudo];

            //Generate NPCs
            var npcsNb = Application.Instance.Randomizer.Next(2, 11);
            for (var i = 0; i < npcsNb; i++)
            {
                Suspects.Add(new Npc());
            }
            //--


            //Wait for all npcs to be create - Add to avoid a bug where murderer was choose but the generation wasn't over 
            while (Suspects.Count < npcsNb)
            {
            }
        }

        //This finalize generation, this is in an over method to avoid the precedently said bug
        public void SetupFinalization()
        {
            //Select the Murderer
            var randMurderer = Application.Instance.Randomizer.Next(0, Suspects.Count);
            Murderer = Suspects[randMurderer];
            Debug.WriteLine("THE MURDERER IS : " + Murderer.Name);

            //Set the known info by each npc
            //Did in a clone list to avoid list editing in for
            var newNpcs = new List<Npc>();
            for (var index = 0; index < Suspects.Count; index++)
            {
                var suspect = Suspects[index];
                var newSuspect = suspect;
                newSuspect.SetInfo();
                newNpcs.Add(newSuspect);
            }
            Suspects = newNpcs;

            //Generate rooms
            var roomsNb = Application.Instance.Randomizer.Next(1, 6);
            for (var i = 0; i < roomsNb; i++)
            {
                Rooms.Add(new Room());
            }
        }
    }
}