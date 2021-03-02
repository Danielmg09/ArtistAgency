using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.DTOs
{
    public class ActorDTO : ClientDTO
    {
        public List<MovieDTO> MovieList { get; set; }

        public ActorDTO()
        {
            MovieList = new List<MovieDTO>();
        }
    }
}
