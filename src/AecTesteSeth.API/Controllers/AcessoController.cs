using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AeCTesteSeth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        public AcessoController(IConfiguration Configuration, IUnitOfWork unitOfWork)
        {
            _config = Configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login login, [FromServices] MyContext context)
        {
            bool resultado = Validar(login,context);
            if (resultado)
            {
                var tokenString = GerarTokenJWT();
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GerarTokenJWT()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience,
expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        private bool Validar(Login login,MyContext context)
        {
          var user =  context.Usuarios.Where(x => x.Usuario_ == login.Usuario && x.Senha == login.Senha).First();
            
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
