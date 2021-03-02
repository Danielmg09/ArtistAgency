using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.DTOs
{
    public class MovieDTO : JobDTO
    {
        public string Title { get; set; }
        public string Genre { get; set; }

        public MovieDTO()
        {
        }
    }
}
