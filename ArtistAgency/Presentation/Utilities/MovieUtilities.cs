using ArtistAgency.DTOs;
using ArtistAgency.Services.DtoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation.Utilities
{
    public static class MovieUtilities
    {
       

        public static void addMovie()
        {
            string title = GeneralUtilities.askForString("Title:");
            string genre = GeneralUtilities.askForString("Genre:");
            decimal salary = GeneralUtilities.askForDecimal("Salary:") ?? 0;

            MovieDTO movie = new MovieDTO()
            {
                Completed = false,
                Assigned = false,
                Removed = false,
                Salary = salary,
                Title = title,
                Genre = genre
            };

            JobServiceDTO.AddMovie(movie);
        }
        public static void editMovie()
        {
            showMoviesList();
            MovieDTO movieToUpdate = getMovieById(GeneralUtilities.askForInt("Select an id:") ?? 0);
            bool finished = false;
            if (movieToUpdate != null)
            {
                do
                {
                    Console.Clear();
                    showMovieInfo(movieToUpdate);
                    showEditMovieOptions();
                    finished = SelectEditMovieOption(movieToUpdate, finished);
                }
                while (finished == false);
                JobServiceDTO.EditMovie(movieToUpdate);
            }
            else
            {
                Console.WriteLine("The id introduced is not correct");
            }

        }
        public static bool SelectEditMovieOption(MovieDTO movie, bool finish)
        {
            string option = GeneralUtilities.askForString("Choose an option: ");
            switch (option)
            {
                case "1":
                    movie.Title = GeneralUtilities.askForString("Title: ");
                    break;
                case "2":
                    movie.Genre = GeneralUtilities.askForString("Genre: ");
                    break;
                case "3":
                    movie.Salary = GeneralUtilities.askForDecimal("Salary: ");
                    break;
                case "4":
                    return true;
            }
            return false;
        }
        public static void showEditMovieOptions()
        {
            Console.WriteLine("1 - Title");
            Console.WriteLine("2 - Genre");
            Console.WriteLine("3 - Salary");
            Console.WriteLine("4 - Finish");
        }
        public static void removeMovie()
        {
            showMoviesList();
            MovieDTO movieToRemove = getMovieById(GeneralUtilities.askForInt("Select an id:") ?? 0);
            if(movieToRemove.Id == 0)
            {
                Console.WriteLine("The id introduced does not match with any movie");
            }
            else
            {
                Console.WriteLine($"The movie {movieToRemove.Title} has been removed");
                JobServiceDTO.RemoveMovie(movieToRemove);
            }
        }
        public static void assignMovie()
        {
            showAvailableMoviesList();
            MovieDTO movie = getMovieById(GeneralUtilities.askForInt("Choose an id from the list: ") ?? 0);
            ActorUtilities.showActorList();
            ActorDTO actor = ActorUtilities.getActorById(GeneralUtilities.askForInt("Choose an id from the list: ") ?? 0);
            if(actor == null || movie == null || movie.Assigned == true)
            {
                Console.WriteLine("The id introduced is not correct");
            }
            else
            {
                JobServiceDTO.AssignMovie(movie, actor);
                Console.WriteLine($"The movie {movie.Title} has been asigned to {actor.FirstName} {actor.LastName}");
            }

        }

        public static MovieDTO getMovieById(int jobId)
        {
            return (MovieDTO)JobServiceDTO.JobList.Find(x => x.Id == jobId);
        }
        public static void showMoviesList()
        {
            foreach (JobDTO j in JobServiceDTO.JobList)
            {
                if (j is MovieDTO && j.Removed == false)
                {
                    Console.WriteLine($"Movie : Id: {j.Id} / Title : {((MovieDTO)j).Title} / Genre : {((MovieDTO)j).Genre} / Salary: {j.Salary}");
                }
            }
        }
        public static void showAvailableMoviesList()
        {
            foreach (JobDTO j in JobServiceDTO.JobList)
            {
                if (j is MovieDTO && j.Removed == false && j.Assigned == false)
                {
                    Console.WriteLine($"Id: {j.Id} / Title : {((MovieDTO)j).Title} / Genre : {((MovieDTO)j).Genre} / Salary: {j.Salary}");
                }
            }
        }
        public static void showMovieById()
        {
            int jobId = GeneralUtilities.askForInt("Write an id:") ?? 0;

            MovieDTO movie = (MovieDTO)JobServiceDTO.JobList.Find(x => x.Id == jobId);
            
            if (movie != null && movie.Removed == false)
            {
                Console.WriteLine($"Id: {movie.Id}");
                Console.WriteLine($"Title : {movie.Title}");
                Console.WriteLine($"Genre : {movie.Genre}");
                Console.WriteLine($"Salary: {movie.Salary}");
                Console.WriteLine($"Completed: {movie.Completed}");
                Console.WriteLine($"Assigned: {movie.Assigned}");
            }
            else
            {
                Console.WriteLine($"There is not any movie with id: {jobId}");
            }

        }
        public static void showMovieInfo(MovieDTO movie)
        {
            Console.WriteLine($"Id: {movie.Id}");
            Console.WriteLine($"Title : {movie.Title}");
            Console.WriteLine($"Genre : {movie.Genre}");
            Console.WriteLine($"Salary: {movie.Salary}");
            Console.WriteLine($"Completed: {movie.Completed}");
            Console.WriteLine($"Assigned: {movie.Assigned}");
        }


        
    }
}
