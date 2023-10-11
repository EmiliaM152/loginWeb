using fsw.web.DTOs.Responses;
using fsw.web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using fsw.web.Repositories.Entities;
using fsw.web.Repositories.Configurations;
using fsw.web.DTOs;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Transactions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using fsw.web.DTOs.Request;

namespace fsw.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext Context;
        private readonly IConfiguration Configuration;

        public UsersController(AppDbContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }


        private string encripter(string texto)
        {
            SHA512 sha512 = SHA512.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] textoEnBytes = sha512.ComputeHash(encoding.GetBytes(texto));
            StringBuilder textoEncriptado = new StringBuilder();
            for (int i = 0; i < textoEnBytes.Length; i++) textoEncriptado.AppendFormat("{0:x2}", textoEnBytes[i]);
            return textoEncriptado.ToString();
        }
        private LoginResponseDTO Token(UserDTO user)
        {

            try
            {
                var expires = DateTime.UtcNow.AddHours(16);
                var claims = new List<Claim>()
                {
                    new Claim("SystemName", user.SysteName)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var securityToken = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expires,
                    signingCredentials: credentials);
                return new LoginResponseDTO
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseDTO
                {
                    Token = "ERROR: " + ex.Message
                };
            }
        }
        [HttpPost("login")]
        public async ValueTask<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO login)
        {
            try
            {
                var ecryptedPassword = encripter(login.Password);
                var query = from tUsers in Context.Users
                            where tUsers.Name == login.Username && tUsers.Password == ecryptedPassword
                            select new UserDTO
                            {
                                id = tUsers.id,
                                SysteName = tUsers.Name,
                                Password = "X"
                            };
                var user = await query.FirstAsync();
                if(user == null)
                {
                    return NotFound($"No ha sido posible identidficar");
                }
                return Ok(Token(user as UserDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
          
    [HttpPost("registration")]
        public async ValueTask<ActionResult<LoginResponseDTO>> Crear([FromBody] LoginRequestDTO login)
        {
            try
            {
                var ecryptedPassword = encripter(login.Password);
                var usuario = new User { Name = login.Username, Password = login.Password };
                await Context.Users.AddAsync(usuario);
                await Context.SaveChangesAsync();
                var query = from tUsers in Context.Users
                            where tUsers.Name == login.Username
                            select new UserDTO
                            {
                                id = tUsers.id,
                                SysteName = tUsers.Name,
                                Password = "X"
                            };
                var nuevoUsuario = await query.FirstAsync();
                return Ok(Token(nuevoUsuario));
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
