using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistAgency.DTOs
{
    public class UserDTO
    {
        public static int Incrementer { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Removed { get; set; }


        public UserDTO()
        {
            Removed = false;
        }
    }
}
