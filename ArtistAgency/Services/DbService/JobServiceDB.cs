using ArtistAgency.Data;
using ArtistAgency.DTOs;
using ArtistAgency.Services.DtoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Services.DbService
{
    public static class JobServiceDB
    {
        public static agencydbEntities1 DBaccess { get; set; } = new agencydbEntities1();

        public static List<JobDTO> loadJobListfromDB(agencydbEntities1 DBAccess)
        {
            List<JobDTO> result = new List<JobDTO>();

            foreach(Movie m in DBAccess.Movie)
            {
                if(m.Job.removed == false)
                {
                    MovieDTO mDto = mapMovieDTOfromDB(m);
                    JobServiceDTO.AddJob(mDto);
                }
                
            }

            foreach (Concert c in DBAccess.Concert)
            {
                if(c.Job.removed == false)
                {
                    ConcertDTO cDto = mapConcertDTOfromDB(c);
                    JobServiceDTO.AddJob(cDto);
                }
                
            }

            return result;
        }

        #region job
        public static void setJobAssigned(JobDTO j)
        {
            DBaccess.Job.FirstOrDefault(x => x.id == j.Id).assigned = true;
            DBaccess.SaveChanges();
        }
        public static void setJobCompleted(JobDTO j)
        {
            DBaccess.Job.FirstOrDefault(x => x.id == j.Id).completed = true;
            DBaccess.SaveChanges();
        }
        public static int? getActoridByMovie(int movieId)
        {
            int? actorId = null;

            foreach (UserDTO u in UserServiceDTO.UserList)
            {
                if (u is ActorDTO)
                {
                    foreach (MovieDTO m in ((ActorDTO)u).MovieList)
                    {
                        if (movieId == m.Id)
                        {
                            actorId = u.Id;
                            return actorId;
                        }
                    }
                }


            }
            return actorId;
        }
        public static int? getMusicianIdByConcert(int concertId)
        {
            int? musicianId = null;
            foreach (UserDTO u in UserServiceDTO.UserList)
            {
                if (u is MusicianDTO)
                {
                    foreach (ConcertDTO c in ((MusicianDTO)u).ConcertList)
                    {
                        if (concertId == c.Id)
                        {
                            musicianId = u.Id;
                            return musicianId;
                        }
                    }
                }
            }
            return musicianId;
        }
        #endregion

        #region Movie
        public static MovieDTO mapMovieDTOfromDB(Movie movieDB)
        {
            MovieDTO movieDTO = new MovieDTO();

            movieDTO.Id = movieDB.Job.id;
            movieDTO.Salary = movieDB.Job.salary ?? 0;
            movieDTO.Title = movieDB.title;
            movieDTO.Genre = movieDB.genre;
            movieDTO.Assigned = movieDB.Job.assigned ?? false;
            movieDTO.Completed = movieDB.Job.completed ?? false;
            movieDTO.Removed = movieDB.Job.removed ?? false;

            return movieDTO;
        }
        public static Movie mapMovieDBfromDTO(MovieDTO movieDTO, Movie movieDB = null)
        {
            if(movieDB == null)
            {
                movieDB = new Movie();
                movieDB.Job = new Job();
            }
            
            movieDB.title = movieDTO.Title;
            movieDB.genre = movieDTO.Genre;
            movieDB.Job.assigned = movieDTO.Assigned;
            movieDB.Job.salary = movieDTO.Salary;
            movieDB.Job.completed = movieDTO.Completed;
            movieDB.Job.removed = movieDTO.Removed;
            movieDB.idActor = getActoridByMovie(movieDTO.Id);

            return movieDB;
        }
        public static int addMovie(MovieDTO movieDTO)
        {
            Movie movieDB = mapMovieDBfromDTO(movieDTO,null);
            JobServiceDB.DBaccess.Movie.Add(movieDB);
            JobServiceDB.DBaccess.SaveChanges();

            return movieDB.Job.id;
        }
        public static int editMovie(MovieDTO movieDTO)
        {
            Movie movieDB = getMovieById(movieDTO.Id);
            mapMovieDBfromDTO(movieDTO, movieDB);
            DBaccess.SaveChanges();
            return movieDB.Job.id;

        }
        public static void removeMovie(MovieDTO movieDTO)
        {
            Movie movieDB = getMovieById(movieDTO.Id);
            movieDB.Job.removed = true;
            JobServiceDB.DBaccess.SaveChanges();
        }
        public static void assignMovie(MovieDTO movieDTO, ActorDTO actorDTO)
        {
            DBaccess.Movie.FirstOrDefault(x => x.idJob == movieDTO.Id).Job.assigned = true;
            getMovieById(movieDTO.Id).idActor = UserServiceDB.getActorById(actorDTO.Id).id;
            //DBaccess.Movie.FirstOrDefault(x => x.idJob == movieDTO.Id).idActor = (DBaccess.Actor.FirstOrDefault(x => x.Client.User.id == actorDTO.Id).id);
            DBaccess.SaveChanges();
        }
        public static Movie getMovieById(int id)
        {
            Movie movie = DBaccess.Movie.FirstOrDefault(x => x.Job.id == id);
            return movie;
        }
        #endregion

        #region Concert
        public static ConcertDTO mapConcertDTOfromDB(Concert concertDB)
        {
            ConcertDTO concertDTO = new ConcertDTO();

            concertDTO.Id = concertDB.Job.id;
            concertDTO.Salary = concertDB.Job.salary ?? 0;
            concertDTO.Venue = concertDB.venue;
            concertDTO.City = concertDB.city;
            concertDTO.Assigned = concertDB.Job.assigned ?? false;
            concertDTO.Completed = concertDB.Job.completed ?? false;
            concertDTO.Removed = concertDB.Job.removed ?? false;

            return concertDTO;
        }
        public static Concert mapConcertDBfromDTO(ConcertDTO concertDTO, Concert concertDB = null)
        {
            if(concertDB == null)
            {
                concertDB = new Concert();
                concertDB.Job = new Job();
            }

            concertDB.Job.salary = concertDTO.Salary;
            concertDB.venue = concertDTO.Venue;
            concertDB.city = concertDTO.City;
            concertDB.Job.assigned = concertDTO.Assigned;
            concertDB.Job.completed = concertDTO.Completed;
            concertDB.Job.removed = concertDTO.Removed;
            concertDB.idMusician = getMusicianIdByConcert(concertDTO.Id);

            return concertDB;

        }
        public static int addConcert(ConcertDTO concertDTO)
        {
            Concert concertDB = mapConcertDBfromDTO(concertDTO,null);
            JobServiceDB.DBaccess.Concert.Add(concertDB);
            JobServiceDB.DBaccess.SaveChanges();

            return concertDB.Job.id;
        }
        public static void editconcert(ConcertDTO concertDTO)
        {
            Concert concertDB = getConcertById(concertDTO.Id);
            mapConcertDBfromDTO(concertDTO,concertDB);
            DBaccess.SaveChanges();
        }
        public static void removeConcert(ConcertDTO concertDTO)
        {
            Concert concertDB = getConcertById(concertDTO.Id);
            concertDB.Job.removed = true;
            JobServiceDB.DBaccess.SaveChanges();
        }
        public static void assignConcert(ConcertDTO concertDTO, MusicianDTO musicianDTO)
        {
            DBaccess.Concert.FirstOrDefault(x => x.idJob == concertDTO.Id).Job.assigned = true;
            DBaccess.Concert.FirstOrDefault(x => x.idJob == concertDTO.Id).idMusician = (DBaccess.Musician.FirstOrDefault(x => x.Client.User.id == musicianDTO.Id).id);
            DBaccess.SaveChanges();
        }
        public static Concert getConcertById(int id)
        {
            Concert concert = DBaccess.Concert.FirstOrDefault(x => x.Job.id == id);
            return concert;
        }

        #endregion

 



    }
}
