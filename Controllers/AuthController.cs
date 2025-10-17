using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using tests.Data;
using tests.DTOs;
using tests.Models;
using System.Text;
using tests.Services.IServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.DataProtection;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NuGet.Protocol;
using FluentValidation;
using tests.Repo.UserRepo.DTO;
using tests.Repo.UserRepo.Features;
using tests.Repo.UserRepo;
using tests.Repo.UserRepo.Mapper;

namespace tests.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        AppDbContext context,
        IJwtService jwtService,
        ILogger<AuthController> logger,
        ICrypto crypto,
        IValidator<UserCreateDTO> UserValidator,
        IUsersRepo _userRepo
        ) : ControllerBase
        
    {
        private readonly AppDbContext _context  = context;
        private readonly IJwtService _jwtService  = jwtService;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly ICrypto _crypto = crypto;
        private readonly IValidator<UserCreateDTO> _userValidator = UserValidator;
        private readonly IUsersRepo _userRepo = _userRepo;

        [HttpGet("generate")]
        public async Task<ActionResult<ResponseDTO<TokenResponseDto>>> Login([FromQuery] LoginDto login)
        {

            UserModel? usuario = await _context.User.FirstOrDefaultAsync(x => x.Email == login.Email);
            byte[] salt = usuario!.Salt;
            string hashed = _crypto.Encrypt(login.Password, in salt);
            if (usuario is null ||
                usuario.Password != hashed ||
                !usuario.isActive)
            {
                return BadRequest("User or Password Incorrect");
            }
            _context.Entry(usuario).Collection(x => x.Role).Load();

            string token = _jwtService.CreateToken(usuario, usuario.Role);
            string refreshToken = _jwtService.GenerateRefreshToken();



            TokenResponseDto tokenResponseDto = new TokenResponseDto()
            {
                AccessToken=token,
                RefreshToken=refreshToken
            };

            usuario.RefreshToken = refreshToken;
            usuario.ExpireTimeToken = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
            ResponseDTO<TokenResponseDto> response = new() { Message="token generado", Data=tokenResponseDto };
            _logger.LogCritical("Usuario Autenticado {Id}", usuario.Id); // Logging Here!!
            return Ok(response);

        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO<MapFromUserModule>>> Register(UserCreateDTO userCreate)
        {
            var Validation = this._userValidator.Validate(userCreate);
            if (!Validation.IsValid) {
              return BadRequest($"Error de Validacion ");
            }
          MapFromUserModule newUser = await _userRepo.CreateUser(userCreate);
     
            return CreatedAtAction(nameof(Register),
                new ResponseDTO<MapFromUserModule>() {
                   Data = newUser,
                   Message = "Usuario Anadido con Exito"
                }
                );
        }
        
        [HttpGet("refresh")]
        public async Task<ActionResult<ResponseDTO<TokenResponseDto>>> RefreshToken([FromQuery] string refreshToken, [FromQuery] int id)
        {
            UserModel? user = await _context.User.FirstOrDefaultAsync(model => model.Id == id);
            if (user is null || !user.isActive) { return BadRequest("Invalid User ID"); }

            if (
                user.RefreshToken is null ||
                user.ExpireTimeToken <= DateTime.UtcNow ||
                user.RefreshToken != refreshToken
                )
            {
                return Unauthorized("Refresh Token Invalid");
            }
            _context.Entry(user).Collection(x => x.Role).Load();

            TokenResponseDto tokenResponseDto = new TokenResponseDto()
            {
                AccessToken = _jwtService.CreateToken(user,user.Role),
                RefreshToken = user.RefreshToken
            };

            return new ResponseDTO<TokenResponseDto>() { Data= tokenResponseDto, Message="Token Generado" };
        }
    }
}
