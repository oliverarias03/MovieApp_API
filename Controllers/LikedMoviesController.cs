using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using MovieAPI.Repositories.Interfaces;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikedMoviesController : BaseController<LikedMovies>
    {
        private readonly ILikedMoviesRepository _likedMoviesRepository;

        public LikedMoviesController(ILikedMoviesRepository likedMoviesRepository)
        {
            this._likedMoviesRepository = likedMoviesRepository;
        }


        [HttpGet("GetMovies")]
        public IActionResult GetMovies()
        {
            return Ok(this._likedMoviesRepository.GetAll().ToList());
        }


        [HttpGet("GetMoviesByUser")]
        public IActionResult GetMoviesByUser(int userId)
        {
            var movies = this._likedMoviesRepository.GetAllBy(u => u.UserId == userId).ToList();

            if (movies == null)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpPost("Create")]
        public override IActionResult Create(LikedMovies entity)
        {

            if (this._likedMoviesRepository.Exists(x => x.MovieId.Equals(entity.MovieId) && x.UserId == entity.UserId))
            {
                return BadRequest("Titulo fue Guardado");
            }
            else
            {
                entity.Id = 0;
                var res = this._likedMoviesRepository.Add(entity);

                return Ok(res);
            }
        }

        [HttpPut("Editar")]
        public override IActionResult Edit(LikedMovies entity)
        {

            var res = this._likedMoviesRepository.Update(entity);

            return Ok(res);
            
        }

        [HttpPost("Eliminar")]
        public IActionResult Eliminar(LikedMovies entity)
        {
            if (!this._likedMoviesRepository.Exists(x => x.MovieId.Equals(entity.MovieId) && x.UserId == entity.UserId))
            {
                return BadRequest("Titulo no existente");
            }
            else
            {
                LikedMovies u = this._likedMoviesRepository.Find(x => x.MovieId.Equals(entity.MovieId) && x.UserId == entity.UserId);

                this._likedMoviesRepository.Remove(u);

                return Ok(0);
            }

        }

    }
}
