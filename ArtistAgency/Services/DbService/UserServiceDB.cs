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
    public static class UserServiceDB
    {
        public static void loadUserListfromDB(agencydbEntities1 DBAccess)
        {
            foreach (Actor a in DBAccess.Actor)
            {
                if(a.Client.User.removed == false)
                {
                    ActorDTO aDto = mapActorDTOfromDB(a);
                    UserServiceDTO.AddUser(aDto);
                }
                
            }

            foreach (Musician m in DBAccess.Musician)
            {
                if(m.Client.User.removed == false)
                {
                    MusicianDTO mDto = mapMusicianDTOfromDB(m);
                    UserServiceDTO.AddUser(mDto);
                }
                
            }
        }

        #region user
        public static void getPaid(UserDTO u, JobDTO j)
        {
            JobServiceDB.DBaccess.Client.FirstOrDefault(x => x.userId == u.Id).income += j.Salary;
            JobServiceDB.DBaccess.SaveChanges();
        }
        #endregion

        #region Actor
        public static ActorDTO mapActorDTOfromDB(Actor actorDB)
        {
            ActorDTO actorDTO = new ActorDTO();

            actorDTO.Id = actorDB.Client.User.id;
            actorDTO.FirstName = actorDB.Client.User.firstName;
            actorDTO.LastName = actorDB.Client.User.lastName;
            actorDTO.Income = actorDB.Client.income ?? 0;
            actorDTO.MovieList = getMovieListbyActor(actorDB);
            actorDTO.Removed = actorDB.Client.User.removed ?? false;

            return actorDTO;
        }
        public static Actor mapActorDBfromDTO(ActorDTO actorDTO,Actor actorDB= null)
        {
            if(actorDB == null)
            {
                actorDB = new Actor();
                actorDB.Client = new Client();
                actorDB.Client.User = new User();
            }
            

            actorDB.Client.User.firstName = actorDTO.FirstName;
            actorDB.Client.User.lastName = actorDTO.LastName;
            actorDB.Client.income = actorDTO.Income;
            actorDB.Client.User.removed = actorDTO.Removed;

            return actorDB;
        }
        public static List<MovieDTO> getMovieListbyActor(Actor actorDB)
        {
            List<MovieDTO> movies = new List<MovieDTO>();

            foreach (Movie m in JobServiceDB.DBaccess.Movie)
            {
                if (m.idActor == actorDB.id)
                {
                    MovieDTO movieDTO = JobServiceDB.mapMovieDTOfromDB(m);
                    movies.Add(movieDTO);
                }
            }

            return movies;
        }
        public static int addActor(ActorDTO actorDTO)
        {
            Actor actorDB = new Actor();
            actorDB = mapActorDBfromDTO(actorDTO);
            JobServiceDB.DBaccess.Actor.Add(actorDB);
            JobServiceDB.DBaccess.SaveChanges();

            return actorDB.Client.User.id;
        }
        public static int editActor(ActorDTO actorDTO)
        {
            Actor actorDB = getActorById(actorDTO.Id);
            mapActorDBfromDTO(actorDTO, actorDB);
            JobServiceDB.DBaccess.SaveChanges();
            return actorDB.Client.userId;
        }
        public static void removeActor(ActorDTO actorDTO)
        {
            Actor actorDB = getActorById(actorDTO.Id);
            actorDB.Client.User.removed = true;
            JobServiceDB.DBaccess.SaveChanges();
        }
        public static Actor getActorById(int id)
        {
            Actor actor = JobServiceDB.DBaccess.Actor.FirstOrDefault(x => x.Client.userId == id);
            return actor;
        }
        public static void quitMovie(ActorDTO actorDTO,MovieDTO movieDTO)
        {
            JobServiceDB.getMovieById(movieDTO.Id).Job.assigned = false;
            JobServiceDB.getMovieById(movieDTO.Id).idActor = null;
            JobServiceDB.DBaccess.SaveChanges();
        }
        

        #endregion

        #region Musician
        public static MusicianDTO mapMusicianDTOfromDB(Musician musicianDB)
        {
            MusicianDTO musicianDTO = new MusicianDTO();

            musicianDTO.Id = musicianDB.Client.User.id;
            musicianDTO.FirstName = musicianDB.Client.User.firstName;
            musicianDTO.LastName = musicianDB.Client.User.lastName;
            musicianDTO.Income = musicianDB.Client.income ?? 0;
            musicianDTO.ConcertList = getConcertListbyMusician(musicianDB);
            musicianDTO.Removed = musicianDB.Client.User.removed ?? false;

            return musicianDTO;
        }
        public static Musician mapMusicianDBfromDTO(MusicianDTO musicianDTO,Musician musicianDB = null)
        {
            if(musicianDB == null)
            {
                musicianDB = new Musician();
                musicianDB.Client = new Client();
                musicianDB.Client.User = new User();
            }
            
            musicianDB.Client.User.firstName = musicianDTO.FirstName;
            musicianDB.Client.User.lastName = musicianDTO.LastName;
            musicianDB.Client.income = musicianDTO.Income;
            musicianDB.Client.User.removed = musicianDTO.Removed;


            return musicianDB;
        }
        public static List<ConcertDTO> getConcertListbyMusician(Musician musicianDB)
        {
            List<ConcertDTO> concerts = new List<ConcertDTO>();

            foreach (Concert c in JobServiceDB.DBaccess.Concert)
            {
                if (c.idMusician == musicianDB.id)
                {
                    ConcertDTO concertDTO = JobServiceDB.mapConcertDTOfromDB(c);
                    concerts.Add(concertDTO);
                }
            }

            return concerts;
        }
        public static int addMusician(MusicianDTO musicianDTO)
        {
            Musician musicianDB = new Musician();
            musicianDB = mapMusicianDBfromDTO(musicianDTO);
            JobServiceDB.DBaccess.Musician.Add(musicianDB);
            JobServiceDB.DBaccess.SaveChanges();

            return musicianDB.Client.User.id;
        }
        public static int editMusician(MusicianDTO musicianDTO)
        {
            Musician musicianDB = getMusicianById(musicianDTO.Id);
            mapMusicianDBfromDTO(musicianDTO, musicianDB);
            JobServiceDB.DBaccess.SaveChanges();
            return musicianDB.Client.userId;
        }
        public static void removeMusician(MusicianDTO musicianDTO)
        {
            Musician musicianDB = getMusicianById(musicianDTO.Id);
            musicianDB.Client.User.removed = true;
            JobServiceDB.DBaccess.SaveChanges();
        }
        public static Musician getMusicianById(int id)
        {
            Musician musician = JobServiceDB.DBaccess.Musician.FirstOrDefault(x => x.Client.userId == id);
            return musician;
        }
        public static void quitConcert(MusicianDTO musicianDTO, ConcertDTO concertDTO)
        {
            JobServiceDB.getConcertById(concertDTO.Id).Job.assigned = false;
            JobServiceDB.getConcertById(concertDTO.Id).idMusician = null;
            JobServiceDB.DBaccess.SaveChanges();
        }


        #endregion Musician





    }
}
