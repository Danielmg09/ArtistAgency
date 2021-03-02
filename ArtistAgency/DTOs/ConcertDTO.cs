using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.DTOs
{
    public class ConcertDTO : JobDTO
    {
        public string Venue { get; set; }
        public string City { get; set; }

        public ConcertDTO()
        {

        }

    }
}
