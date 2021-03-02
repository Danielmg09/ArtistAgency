using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.DTOs
{
    public class ClientDTO : UserDTO
    {
        public decimal? Income { get; set; }

        public ClientDTO()
        {
        }
    }
}
