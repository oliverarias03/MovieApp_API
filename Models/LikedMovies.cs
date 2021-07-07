using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MovieAPI.Models
{
    public partial class LikedMovies
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string MovieId { get; set; }
        public string MovieType { get; set; }

        public virtual Users User { get; set; }
    }
}
