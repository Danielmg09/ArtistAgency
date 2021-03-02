using ArtistAgency.DTOs;
using ArtistAgency.Services.DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Services.DtoService
{
    public static class UserServiceDTO
    {
        public static List<UserDTO> UserList { get; set; } = new List<UserDTO>();

        #region user
        public static void AddUser(UserDTO u)
        {
            UserList.Add(u);
        }
        public static void DeleteUser(UserDTO u)
        {
            UserList.Remove(u);
        }
        public static UserDTO GetUserById(int id)
        {
            UserDTO user = null;

            foreach (UserDTO u in UserList)
            {
                if (u.Id == id)
                {
                    user = u;
                }
            }
            return user;
        }
        public static void JobToClientList(JobDTO j, UserDTO u)
        {

            if (u is ActorDTO)
            {
                ((ActorDTO)(UserList.Find(x => x.Id == u.Id))).MovieList.Add((MovieDTO)j);
            }
            if (u is MusicianDTO)
            {
                ((MusicianDTO)(UserList.Find(x => x.Id == u.Id))).ConcertList.Add((ConcertDTO)j);
            }

        }
        public static void GetPaid(UserDTO u, JobDTO j)
        {
            ((ClientDTO)(UserList.Find(x => x.Id == u.Id))).Income += j.Salary;
            UserServiceDB.getPaid(u, j);
        }
        public static void PerformJob(UserDTO u, JobDTO j)
        {
            JobServiceDTO.SetJobCompleted(u,j);
            GetPaid(u, j);

        }

        #endregion

        #region actor
        public static void AddActor(ActorDTO a)
        {
            a.Id = UserServiceDB.addActor(a);
            UserList.Add(a);
        }
        public static void RemoveActor(ActorDTO a)
        {
            QuitNotCompletedMovies(a);
            a.Removed = true;
            UserServiceDB.removeActor(a);
        }
        public static void EditActor(ActorDTO a)
        {
            UserServiceDB.editActor(a);
        }

        public static void QuitNotCompletedMovies(ActorDTO a)
        {
            foreach(MovieDTO m in a.MovieList)
            {
                if(m.Completed == false)
                {
                    m.Assigned = false;
                    UserServiceDB.quitMovie(a, m);
                }

            }
        }
        public static void QuitMovie(ActorDTO a, MovieDTO m)
        {
            m.Assigned = false;
            a.MovieList.Remove(a.MovieList.FirstOrDefault(x => x.Id == m.Id));
            UserServiceDB.quitMovie(a, m);
        }
       
        #endregion

        #region musician
        public static void AddMusician(MusicianDTO m)
        {
            m.Id = UserServiceDB.addMusician(m);
            UserList.Add(m);
        }
        public static void RemoveMusician(MusicianDTO m)
        {
            QuitNotCompletedconcerts(m);
            m.Removed = true;
            UserServiceDB.removeMusician(m);
        }
        public static void EditMusician(MusicianDTO m)
        {
            UserServiceDB.editMusician(m);
        }

        public static void QuitNotCompletedconcerts(MusicianDTO m)
        {
            foreach (ConcertDTO c in m.ConcertList)
            {
                if (c.Completed == false)
                {
                    c.Assigned = false;
                    UserServiceDB.quitConcert(m, c);
                }

            }
        }
        public static void QuitConcert(MusicianDTO m, ConcertDTO c)
        {
            c.Assigned = false;
            m.ConcertList.Remove(m.ConcertList.FirstOrDefault(x => x.Id == c.Id));
            UserServiceDB.quitConcert(m, c);
        }


        #endregion



    }
}
