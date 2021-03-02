using ArtistAgency.DTOs;
using ArtistAgency.Services.DbService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.Services.DtoService
{
    public static class JobServiceDTO
    {
        public static List<JobDTO> JobList { get; set; } = new List<JobDTO>();

        #region Job
        public static void AddJob(JobDTO j)
        {
            JobList.Add(j);
        }
        public static void SetJobCompleted(UserDTO u, JobDTO j)
        {
            j.Completed = true;
            JobServiceDB.setJobCompleted(j);
        }
        #endregion

        #region Movie
        public static void AddMovie(MovieDTO m)
        {
            m.Id = JobServiceDB.addMovie(m);
            JobList.Add(m);
        }
        public static void EditMovie(MovieDTO m)
        {
            JobServiceDB.editMovie(m);
        }
        public static void RemoveMovie(MovieDTO m)
        {
            m.Removed = true;
            JobServiceDB.removeMovie(m);
        }
        public static void AssignMovie(MovieDTO m, ActorDTO a)
        {
            m.Assigned = true;
            a.MovieList.Add(m);
            JobServiceDB.assignMovie(m, a);
        }

        #endregion

        #region Concert
        public static void AddConcert(ConcertDTO c)
        {
            c.Id = JobServiceDB.addConcert(c);
            JobList.Add(c);
        }
        public static void EditConcert(ConcertDTO c)
        {


        }
        public static void RemoveConcert(ConcertDTO c)
        {
            c.Removed = true;
            JobServiceDB.removeConcert(c);
        }
        public static void AssignConcert(ConcertDTO c, MusicianDTO m)
        {
            c.Assigned = true;
            m.ConcertList.Add(c);
            JobServiceDB.assignConcert(c, m);
        }
        #endregion








    }
}
