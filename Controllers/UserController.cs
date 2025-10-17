using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tests.Data;
using tests.DTOs;
using tests.Models;
using tests.Repo.UserRepo;
using tests.Repo.UserRepo.DTO;
using tests.Repo.UserRepo.Mapper;

namespace tests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUsersRepo repoUser) : ControllerBase
    {
       
        private IUsersRepo _repoUser = repoUser;

       // [Authorize(Roles ="ADMIN")]
        [HttpGet]
        public async Task<ResponseDTO<MapFromUserModule[]>> GetUser()
        {
            //return await _context.User.ToListAsync();
            return new()
            {
                Message= "Usuarios en el Sistema",
                Data = await  this._repoUser.GetUsers()
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO<MapFromUserModule>>> GetUserById(int id)
        {
            var userModel = await _repoUser.GetUserById(id);

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(new ResponseDTO<MapFromUserModule>()
            {
                Data = userModel,
                Message = "Usuario Encontrado con Exito"
            });
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDTO<MapFromUserModule>>> DeleteUserModel(int id)
        {
            var userModel = await _repoUser.DeleteUser(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return Ok(new ResponseDTO<MapFromUserModule>() {Data=userModel,Message="Eliminado Con Exito"});
        }

        [HttpGet("lastseen/{id}")]
        public string? LastSeen( int id)
        {
           return  this._repoUser.LastSeen(id);
            
        }
    }
}
