using ArtistAgency.DTOs;
using ArtistAgency.Services.DtoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation.Utilities
{
    public static class ConcertUtilities
    {
        #region concert

        public static void addConcert()
        {
            string city = GeneralUtilities.askForString("City:");
            string venue = GeneralUtilities.askForString("Venue:");
            decimal salary = GeneralUtilities.askForDecimal("Salary:") ?? 0;

            ConcertDTO concert = new ConcertDTO
            {
                Completed = false,
                Assigned = false,
                Removed = false,
                City = city,
                Venue = venue,
                Salary = salary
            };

            JobServiceDTO.AddConcert(concert);

        }
        public static void editConcert()
        {
            showConcertsList();
            ConcertDTO concertToUpdate = getConcertById(GeneralUtilities.askForInt("Select an id:") ?? 0);
            bool finished = false;
            if (concertToUpdate != null)
            {
                do
                {
                    Console.Clear();
                    showConcertInfo(concertToUpdate);
                    showEditConcertOptions();
                    finished = SelectEditConcertOption(concertToUpdate, finished);
                }
                while (finished == false);
                JobServiceDTO.EditConcert(concertToUpdate);
            }
            else
            {
                Console.WriteLine("The id introduced is not correct");
            }

        }
        public static bool SelectEditConcertOption(ConcertDTO concert, bool finish)
        {
            string option = GeneralUtilities.askForString("Choose an option: ");
            switch (option)
            {
                case "1":
                    concert.City = GeneralUtilities.askForString("City: ");
                    break;
                case "2":
                    concert.Venue = GeneralUtilities.askForString("Venue: ");
                    break;
                case "3":
                    concert.Salary = GeneralUtilities.askForDecimal("Salary: ");
                    break;
                case "4":
                    return true;
            }
            return false;
        }
        public static void showEditConcertOptions()
        {
            Console.WriteLine("1 - City");
            Console.WriteLine("2 - Venue");
            Console.WriteLine("3 - Salary");
            Console.WriteLine("4 - Finish");
        }
        public static void removeConcert()
        {
            showConcertsList();
            ConcertDTO concertToRemove = getConcertById(GeneralUtilities.askForInt("Select an id:") ?? 0);
            if (concertToRemove.Id == 0 || concertToRemove == null)
            {
                Console.WriteLine("The id introduced does not match with any concert");
            }
            else
            {
                Console.WriteLine($"The concert in {concertToRemove.Venue}, {concertToRemove.City}, has been removed");
                JobServiceDTO.RemoveConcert(concertToRemove);
            }
        }
        public static void assignConcert()
        {
            showAvailableConcertsList();
            ConcertDTO concert = getConcertById(GeneralUtilities.askForInt("Choose an id from the list: ") ?? 0);
            MusicianUtilities.showMusicianList();
            MusicianDTO musician = MusicianUtilities.getMusicianById(GeneralUtilities.askForInt("Choose an id from the list: ") ?? 0);
            if (musician == null || concert == null || concert.Assigned == true)
            {
                Console.WriteLine("The id introduced is not correct");
            }
            else
            {
                JobServiceDTO.AssignConcert(concert, musician);
                Console.WriteLine($"The concert in {concert.Venue}, {concert.City}, has been asigned to {musician.FirstName} {musician.LastName}");
            }

        }


        public static ConcertDTO getConcertById(int jobId)
        {
            return (ConcertDTO)JobServiceDTO.JobList.Find(x => x.Id == jobId);
        }
        public static void showConcertsList()
        {
            foreach (JobDTO j in JobServiceDTO.JobList)
            {
                if (j is ConcertDTO && j.Removed == false)
                {
                    Console.WriteLine($"Concert : Id: {j.Id} / City : {((ConcertDTO)j).City} / Venue : {((ConcertDTO)j).Venue} /Salary: {j.Salary}");
                }
            }
        }
        public static void showAvailableConcertsList()
        {
            foreach (JobDTO j in JobServiceDTO.JobList)
            {
                if (j is ConcertDTO && j.Removed == false && j.Assigned == false)
                {
                    Console.WriteLine($"Concert : Id: {j.Id} / City : {((ConcertDTO)j).City} / Venue : {((ConcertDTO)j).Venue} / Salary: {j.Salary}");
                }
            }
        }
        public static void showConcertById()
        {
            int jobId = GeneralUtilities.askForInt("Write an id:") ?? 0;
            
            ConcertDTO concert = (ConcertDTO)JobServiceDTO.JobList.Find(x => x.Id == jobId);
            if (concert != null && concert.Removed == false)
            {
                Console.WriteLine($"Id: {concert.Id}");
                Console.WriteLine($"City : {concert.City}");
                Console.WriteLine($"Venue : {concert.Venue}");
                Console.WriteLine($"Salary: {concert.Salary}");
                Console.WriteLine($"Completed: {concert.Completed}");
                Console.WriteLine($"Assigned: {concert.Assigned}");
            }
            else
            {
                Console.WriteLine($"There is not any concert with id: {jobId}");
            }  
            
        }
        public static void showConcertInfo(ConcertDTO concert)
        {
            Console.WriteLine($"Id: {concert.Id}");
            Console.WriteLine($"City : {concert.City}");
            Console.WriteLine($"Venue : {concert.Venue}");
            Console.WriteLine($"Salary: {concert.Salary}");
            Console.WriteLine($"Completed: {concert.Completed}");
            Console.WriteLine($"Assigned: {concert.Assigned}");
        }


        #endregion
    }
}
