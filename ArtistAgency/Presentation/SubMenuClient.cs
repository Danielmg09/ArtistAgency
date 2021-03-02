using ArtistAgency.Presentation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation
{
    public class SubMenuClient
    {
        string Option { get; set; }
        bool Finished { get; set; }

       

        public SubMenuClient()
        {
            
        }

        #region ClientSubmenu
        public void showClientsOptions()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Actors");
                Console.WriteLine("2 - Musicians");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose an option: ");
                Option = Console.ReadLine();
                SelectOptionClientsSubmenu();

                if (Finished == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pres any key to continue: ");
                    Console.ReadKey();
                }
            } while (Option != "3");

            Option = "0";
            Finished = false;

        }
        public void SelectOptionClientsSubmenu()
        {

            switch (Option)
            {
                case "1":
                    showActorOptions();
                    break;
                case "2":
                    showMusicianOptions();
                    break;
                case "3":
                    Finished = true;
                    break;
            }

        }
        #endregion

        #region ActorSubmenu
        public void showActorOptions()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Add Actor");
                Console.WriteLine("2 - Edit Actor");
                Console.WriteLine("3 - Remove Actor");
                Console.WriteLine("4 - Assign Job");
                Console.WriteLine("5 - Perform Job");
                Console.WriteLine("6 - Quit Job");
                Console.WriteLine("7 - Actor List");
                Console.WriteLine("8 - Show Actor by Id");
                Console.WriteLine("9 - Movie list by Actor");
                Console.WriteLine("10 - Back");

                Console.WriteLine("Choose an option: ");
                Option = Console.ReadLine();
                SelectOptionActorSubmenu();

                if (Finished == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pres any key to continue: ");
                    Console.ReadKey();
                }
            } while (Option != "10");

            Option = "0";
            Finished = false;

        }
        public void SelectOptionActorSubmenu()
        {

            switch (Option)
            {
                case "1":
                    ActorUtilities.addActor();
                    break;
                case "2":
                    ActorUtilities.editActor();
                    break;
                case "3":
                    ActorUtilities.removeActor();
                    break;
                case "4":
                    MovieUtilities.assignMovie();
                    break;
                case "5":
                    ActorUtilities.performMovie();
                    break;
                case "6":
                    ActorUtilities.quitMovie();
                    break;
                case "7":
                    ActorUtilities.showActorList();
                    break;
                case "8":
                    ActorUtilities.showActorById();
                    break;
                case "9":
                    ActorUtilities.showActorMovies();
                    break;
                case "10":
                    Finished = true;
                    break;
            }
        }
        #endregion

        #region MusicianSubmenu
        public void showMusicianOptions()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Add Musician");
                Console.WriteLine("2 - Edit Musician");
                Console.WriteLine("3 - Remove Musician");
                Console.WriteLine("4 - Assign Job");
                Console.WriteLine("5 - Perform Job");
                Console.WriteLine("6 - Quit Job");
                Console.WriteLine("7 - Musician List");
                Console.WriteLine("8 - Show Musician by Id");
                Console.WriteLine("9 - Concert list by Musician");
                Console.WriteLine("10 - Back");

                Console.WriteLine("Choose an option: ");
                Option = Console.ReadLine();
                SelectOptionMusicianSubmenu();

                if (Finished == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pres any key to continue: ");
                    Console.ReadKey();
                }
            } while (Option != "10");

            Option = "0";
            Finished = false;

        }
        public void SelectOptionMusicianSubmenu()
        {

            switch (Option)
            {
                case "1":
                    MusicianUtilities.addMusician();
                    break;
                case "2":
                    MusicianUtilities.editMusician();
                    break;
                case "3":
                    MusicianUtilities.removeMusician();
                    break;
                case "4":
                    ConcertUtilities.assignConcert();
                    break;
                case "5":
                    MusicianUtilities.performConcert();
                    break;
                case "6":
                    MusicianUtilities.quitConcert();
                    break;
                case "7":
                    MusicianUtilities.showMusicianList();
                    break;
                case "8":
                    MusicianUtilities.showMusicianById();
                    break;
                case "9":
                    MusicianUtilities.showMusicianConcerts();
                    break;
                case "10":
                    Finished = true;
                    break;


            }

        }
        #endregion
    }
}
