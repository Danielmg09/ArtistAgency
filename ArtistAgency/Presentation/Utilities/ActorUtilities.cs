using ArtistAgency.DTOs;
using ArtistAgency.Services.DtoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Presentation.Utilities
{
    public static class ActorUtilities
    {
        public static void addActor()
        {
            
            string firstName = GeneralUtilities.askForString("First Name:");
            string lastName = GeneralUtilities.askForString("Last Name:");

            ActorDTO actor = new ActorDTO()
            {
                FirstName = firstName,
                LastName = lastName,
                Income = 0,
                Removed = false,
                MovieList = new List<MovieDTO>()
            };

            UserServiceDTO.AddActor(actor);
        }
        public static void editActor()
        {
            showActorList();
            ActorDTO actorToUpdate = getActorById(GeneralUtilities.askForInt("Select an id:") ?? 0);
            bool finished = false;
            if (actorToUpdate != null)
            {
                do
                {
                    Console.Clear();
                    showActorInfo(actorToUpdate);
                    showEditActorOptions();
                    finished = SelectEditActorOption(actorToUpdate);
                }
                while (finished == false);
                UserServiceDTO.EditActor(actorToUpdate);
            }
            else
            {
                Console.WriteLine("The id introduced is not correct");
            }

        }
        public static bool SelectEditActorOption(ActorDTO actor)
        {
            string option = GeneralUtilities.askForString("Choose an option: ");
            switch (option)
            {
                case "1":
                    actor.FirstName = GeneralUtilities.askForString("First Name: ");
                    break;
                case "2":
                    actor.LastName = GeneralUtilities.askForString("Last Name: ");
                    break;
                case "3":
                    return true;
            }
            return false;
        }
        public static void showEditActorOptions()
        {
            Console.WriteLine("1 - First Name");
            Console.WriteLine("2 - Last Name");
            Console.WriteLine("3 - Finish");
        }
        public static void removeActor()
        {
            showActorList();
            ActorDTO actorToRemove = getActorById(GeneralUtilities.askForInt("Select an id:") ?? 0);
            if (actorToRemove == null)
            {
                Console.WriteLine("The id introduced does not match with any movie");
            }
            else
            {
                Console.WriteLine($"The actor {actorToRemove.FirstName} {actorToRemove.LastName}  has been removed");
                UserServiceDTO.RemoveActor(actorToRemove);
            }

        }

        public static void performMovie()
        {
            showActorList();
            ActorDTO actor = getActorById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
            if (actor != null)
            {
                showActorAvailableMovies(actor);
                MovieDTO movie = MovieUtilities.getMovieById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
                if (movie != null)
                {
                    UserServiceDTO.PerformJob(actor,movie);
                    Console.WriteLine($"{actor.FirstName} {actor.LastName} has performed the movie {movie.Title} and has earned {movie.Salary}");

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
        public static void quitMovie()
        {
            showActorList();
            ActorDTO actor = getActorById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
            if (actor != null)
            {
                showActorAvailableMovies(actor);
                MovieDTO movie = MovieUtilities.getMovieById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
                if (movie != null)
                {
                    UserServiceDTO.QuitMovie(actor, movie);
                    Console.WriteLine($"{actor.FirstName} {actor.LastName} has quit the job at the movie {movie.Title}");

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

        
        public static ActorDTO getActorById(int actorId)
        {
            return (ActorDTO)UserServiceDTO.UserList.Find(x => x.Id == actorId);
        }
        public static void showActorList()
        {
            foreach (UserDTO u in UserServiceDTO.UserList)
            {
                if (u is ActorDTO && u.Removed == false)
                {
                    Console.WriteLine($"Id: {u.Id} / First Name : {u.FirstName} / Last Name: {u.LastName}");
                }
            }
        }
        public static void showActorById()
        {
            int actorId = GeneralUtilities.askForInt("Write an id:") ?? 0;

            ActorDTO actor = (ActorDTO)UserServiceDTO.UserList.Find(x => x.Id == actorId);

            if (actor != null && actor.Removed == false)
            {
                showActorInfo(actor);
            }
            else
            {
                Console.WriteLine($"There is not any movie with id: {actorId}");
            }

        }
        public static void showActorInfo(ActorDTO actor)
        {
            Console.WriteLine($"Id: {actor.Id}");
            Console.WriteLine($"First Name : {actor.FirstName}");
            Console.WriteLine($"Last Name : {actor.LastName}");
            Console.WriteLine($"Income: {actor.Income}");
        }
        public static void showActorMovies()
        {
            showActorList();
            ActorDTO actor = getActorById(GeneralUtilities.askForInt("Choose an id: ") ?? 0);
            if (actor != null)
            {
                foreach (MovieDTO m in actor.MovieList)
                {
                    Console.WriteLine($"Id: {m.Id} / Title: {m.Title} / Genre: {m.Genre} / Salary: {m.Salary} / Completed: {m.Completed}");
                }
            }
            else
            {
                Console.WriteLine("The id introduced is not correct");
            }
            
        }
        public static void showActorAvailableMovies(ActorDTO actor)
        {
            foreach (MovieDTO m in actor.MovieList)
            {
                if(m.Completed == false && m.Removed == false)
                {
                    Console.WriteLine($"Id: {m.Id} / Title: {m.Title} / Genre: {m.Genre} / Salary: {m.Salary}");
                }
                
            }
        }

        
    }
}
