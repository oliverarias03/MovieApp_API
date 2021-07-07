using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
using MovieAPI.Repositories.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;
namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<Users>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IWebHostEnvironment _env;

        public UsersController(IUsersRepository usersRepository, IWebHostEnvironment env)
        {
            this._usersRepository = usersRepository;
            this._env = env;
        }


        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(this._usersRepository.GetAll().ToList());
        }


        [HttpGet("GetUsersById")]
        public IActionResult GetUsersById(int id)
        {
            var users = this._usersRepository.GetAllBy(u => u.Id == id).ToList();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPost("Create")]
        public override IActionResult Create(Users entity)
        {

            if (this._usersRepository.Exists(x => x.Email == entity.Email))
            {
                return BadRequest("Correo Existente");
            }
            else
            {

                entity.Id = 0;
                var res = this._usersRepository.Add(entity);

                return Ok(res);
            }

        }

        [HttpPut("Editar")]
        public override IActionResult Edit(Users entity)
        {
            if (this._usersRepository.Exists(x => x.Email.ToLower() == entity.Email.ToLower() && x.Id != entity.Id))
            {
                return BadRequest("Correo Existente");
            }
            else
            {
                var res = this._usersRepository.Update(entity);

                return Ok(res);
            }
        }

        [HttpDelete("Eliminar")]
        public IActionResult Eliminar(int id)
        {
            if (!this._usersRepository.Exists(x => x.Id == id))
            {
                return BadRequest("Usuario no existente");
            }
            else
            {
                Users u = this._usersRepository.Find(id);

                this._usersRepository.Remove(u);

                return Ok("Usuario Eliminado");
            }

        }

        [HttpPost("Login")]
        public IActionResult Login(Users entity)
        {

            if (!this._usersRepository.Exists(x => x.Email.ToLower() == entity.Email.ToLower() && x.Password == entity.Password))
            {
                return BadRequest("Credenciales Incorrectas");
            }
            else
            {
                Users u = this._usersRepository.Find(x=> x.Email == entity.Email);
                return Ok(u);
            }

        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile(){
            try{

                var request = Request.Form;
                var postedFile = request.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = this._env.ContentRootPath + "/Photos/" + fileName;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);

            }catch(Exception e){
                return new JsonResult("anonymous.png");
            }
        }

    }
}
