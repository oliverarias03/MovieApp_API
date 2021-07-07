using MovieAPI.Models;
using MovieAPI.Repositories.Interfaces;

namespace MovieAPI.Repositories.Implementations
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(Microsoft.EntityFrameworkCore.DbContextOptions<MovieDBContext> options)
        {
            this.Context = new MovieDBContext(options);
        }
    }
}
