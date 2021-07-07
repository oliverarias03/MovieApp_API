using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MovieAPI.Models
{
    public partial class Users
    {
        public Users()
        {
            LikedMovies = new HashSet<LikedMovies>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhotoFileName { get; set; }

        public virtual ICollection<LikedMovies> LikedMovies { get; set; }
    }
}
