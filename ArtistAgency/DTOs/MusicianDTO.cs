using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.DTOs
{
    public class MusicianDTO : ClientDTO
    {
        public List<ConcertDTO> ConcertList { get; set; }

        public MusicianDTO()
        {
            ConcertList = new List<ConcertDTO>();
        }
    }
}
