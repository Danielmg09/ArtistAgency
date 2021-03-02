using ArtistAgency.DTOs;
using ArtistAgency.Services.DtoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation.Utilities
{
    public static class MusicianUtilities
    {
        public static void addMusician()
        {
            string firstName = GeneralUtilities.askForString("First Name:");
            string lastName = GeneralUtilities.askForString("Last Name:");

            MusicianDTO musician = new MusicianDTO()
            {
                FirstName = firstName,
                LastName = lastName,
                Income = 0,
                Removed = false,
                ConcertList = new List<ConcertDTO>()

            };

            UserServiceDTO.AddMusician(musician);
        }
        public static void editMusician()
        {
            showMusicianList();
            MusicianDTO musicianToUpdate = getMusicianById(GeneralUtilities.askForInt("Select an id:") ?? 0);
            bool finished = false;
            if (musicianToUpdate != null)
            {
                do
                {
                    Console.Clear();
                    showMusicianInfo(musicianToUpdate);
                    showEditMusicianOptions();
                    finished = SelectEditMusicianOption(musicianToUpdate);
                }
                while (finished == false);
                UserServiceDTO.EditMusician(musicianToUpdate);
            }
            else
            {
                Console.WriteLine("The id introduced is not correct");
            }

        }
        public static bool SelectEditMusicianOption(MusicianDTO musician)
        {
            string option = GeneralUtilities.askForString("Choose an option: ");
            switch (option)
            {
                case "1":
                    musician.FirstName = GeneralUtilities.askForString("First Name: ");
                    break;
                case "2":
                    musician.LastName = GeneralUtilities.askForString("Last Name: ");
                    break;
                case "3":
                    return true;
            }
            return false;
        }
        public static void showEditMusicianOptions()
        {
            Console.WriteLine("1 - First Name");
            Console.WriteLine("2 - Last Name");
            Console.WriteLine("3 - Finish");
        }
        public static void removeMusician()
        {
            showMusicianList();
            MusicianDTO musicianToRemove = getMusicianById(GeneralUtilities.askForInt("Select an id:") ?? 0);
            if (musicianToRemove.Id == 0)
            {
                Console.WriteLine("The id introduced does not match with any movie");
            }
            else
            {
                Console.WriteLine($"The musician {musicianToRemove.FirstName} {musicianToRemove.LastName}  has been removed");
                UserServiceDTO.RemoveMusician(musicianToRemove);
            }

        }

        public static void performConcert()
        {
            showMusicianList();
            MusicianDTO musician = getMusicianById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
            if (musician != null)
            {
                showMusicianAvailableConcerts(musician);
                ConcertDTO concert = ConcertUtilities.getConcertById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
                if (concert != null)
                {
                    UserServiceDTO.PerformJob(musician, concert);
                    Console.WriteLine($"{musician.FirstName} {musician.LastName} has performed the concert  in {concert.Venue} and has earned {concert.Salary}");
                }
                else
                {
                    Console.WriteLine("The id introduced is not correct");
                }
            }
            else
            {
                Console.WriteLine("The id introduced is not correct");
            }

        }
        public static void quitConcert()
        {
            showMusicianList();
            MusicianDTO musician = getMusicianById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
            if (musician != null)
            {
                showMusicianAvailableConcerts(musician);
                ConcertDTO concert = ConcertUtilities.getConcertById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
                if (concert != null)
                {
                    UserServiceDTO.QuitConcert(musician, concert);
                    Console.WriteLine($"{musician.FirstName} {musician.LastName} has quit the job in {concert.Venue}. {concert.City}");

                }
                else
                {
                    Console.WriteLine("The id introduced is not correct");
                }
            }
            else
            {
                Console.WriteLine("The id introduced is not correct");
            }
        }

        public static MusicianDTO getMusicianById(int musicianId)
        {
            return (MusicianDTO)UserServiceDTO.UserList.Find(x => x.Id == musicianId);
        }
        public static void showMusicianList()
        {
            foreach (UserDTO u in UserServiceDTO.UserList)
            {
                if (u is MusicianDTO && u.Removed == false)
                {
                    Console.WriteLine($"Id: {u.Id} / First Name : {u.FirstName} / Last Name: {u.LastName}");
                }
            }
        }
        public static void showMusicianById()
        {
            int musicianId = GeneralUtilities.askForInt("Write an id:") ?? 0;

            MusicianDTO musician = (MusicianDTO)UserServiceDTO.UserList.Find(x => x.Id == musicianId);

            if (musician != null && musician.Removed == false)
            {
                showMusicianInfo(musician);
            }
            else
            {
                Console.WriteLine($"There is not any movie with id: {musicianId}");
            }

        }
        public static void showMusicianInfo(MusicianDTO musician)
        {
            Console.WriteLine($"Id: {musician.Id}");
            Console.WriteLine($"First Name : {musician.FirstName}");
            Console.WriteLine($"Last Name : {musician.LastName}");
            Console.WriteLine($"Income: {musician.Income}");
        }
        public static void showMusicianConcerts()
        {
            showMusicianList();
            MusicianDTO musician = getMusicianById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
            if (musician != null)
            {
                foreach (ConcertDTO c in musician.ConcertList)
                {
                    Console.WriteLine($"Id: {c.Id} / City: {c.City} / Venue: {c.Venue} / Salary: {c.Salary} / Completed: {c.Completed}");
                }
            }
            else
            {
                Console.WriteLine("The id introduced is not correct");
            }
            
        }
        public static void showMusicianAvailableConcerts(MusicianDTO musician)
        {
            foreach (ConcertDTO c in musician.ConcertList)
            {
                if (c.Completed == false && c.Removed == false)
                {
                    Console.WriteLine($"Id: {c.Id} / City: {c.City} / Venue: {c.Venue} / Salary: {c.Salary}");
                }

            }
        }

    }
}
