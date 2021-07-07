using MovieAPI.Models;
using MovieAPI.Repositories.Interfaces;

namespace MovieAPI.Repositories.Implementations
{
    public class LikedMoviesRepository : Repository<LikedMovies>, ILikedMoviesRepository
    {
        public LikedMoviesRepository(Microsoft.EntityFrameworkCore.DbContextOptions<MovieDBContext> options)
        {
            this.Context = new MovieDBContext(options);
        }
    }
}
