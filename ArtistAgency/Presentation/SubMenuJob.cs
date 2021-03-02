using ArtistAgency.Presentation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation
{
    public class SubMenuJob
    {
        string Option { get; set; }
        bool Finished { get; set; }

       

        public SubMenuJob()
        {
           
        }
        #region JobsSubmenu
        public void showJobOptions()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Movie Options");
                Console.WriteLine("2 - Concert Options");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose an option: ");
                Option = Console.ReadLine();
                SelectOptionJobSubmenu();

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
        public void showMovieOptions()
        {

            do
            {
                Console.Clear();
                Console.WriteLine("1 - Add Movie");
                Console.WriteLine("2 - Edit Movie");
                Console.WriteLine("3 - Assign Movie");
                Console.WriteLine("4 - Delete Movie");
                Console.WriteLine("5 - Movie List");
                Console.WriteLine("6 - Show Movie By Id");
                Console.WriteLine("7 - Movie List by Client");
                Console.WriteLine("8 - Back");

                Console.WriteLine("Choose an option: ");
                Option = Console.ReadLine();
                SelectOptionMovieSubmenu();

                if (Finished == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pres any key to continue: ");
                    Console.ReadKey();
                }
            } while (Option != "8");

            Option = "0";
            Finished = false;

        }
        public void showConcertOptions()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Add Concert");
                Console.WriteLine("2 - Edit Concert");
                Console.WriteLine("3 - Assign Concert");
                Console.WriteLine("4 - Delete Concert");
                Console.WriteLine("5 - Concert List");
                Console.WriteLine("6 - Show Concert By Id");
                Console.WriteLine("7 - Concert List by Client");
                Console.WriteLine("8 - Back");

                Console.WriteLine("Choose an option: ");
                Option = Console.ReadLine();
                SelectOptionConcertSubmenu();

                if (Finished == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Pres any key to continue: ");
                    Console.ReadKey();
                }
            } while (Option != "8");

            Option = "0";
            Finished = false;
        }

        public void SelectOptionJobSubmenu()
        {
            switch (Option)
            {
                case "1":
                    showMovieOptions();
                    break;
                case "2":
                    showConcertOptions();
                    break;
                case "3":
                    Finished = true;
                    break;
                   
            }
        }
        public void SelectOptionMovieSubmenu()
        {

            switch (Option)
            {
                case "1":
                    MovieUtilities.addMovie();
                    break;
                case "2":
                    MovieUtilities.editMovie();
                    break;
                case "3":
                    MovieUtilities.assignMovie();
                    break;
                case "4":
                    MovieUtilities.removeMovie();
                    break;
                case "5":
                    MovieUtilities.showMoviesList();
                    break;
                case "6":
                    MovieUtilities.showMovieById();
                    break;
                case "7":
                    ActorUtilities.showActorMovies();
                    break;
                case "8":
                    Finished = true;
                    break;
            }

        }
        public void SelectOptionConcertSubmenu()
        {

            switch (Option)
            {
                case "1":
                    ConcertUtilities.addConcert();
                    break;
                case "2":
                    ConcertUtilities.editConcert();
                    break;
                case "3":
                    ConcertUtilities.assignConcert();
                    break;
                case "4":
                    ConcertUtilities.removeConcert();
                    break;
                case "5":
                    ConcertUtilities.showConcertsList();
                    break;
                case "6":
                    ConcertUtilities.showConcertById();
                    break;
                case "7":
                    MusicianUtilities.showMusicianConcerts();
                    break;
                case "8":
                    Finished = true;
                    break;
            }
        }
        #endregion
    }
}
