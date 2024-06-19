using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AeCTesteSeth.API.Controllers
{
    
    public class AcessoController : Controller
    {
        private IConfiguration _config;
        private readonly MyContext _myContext;
        public AcessoController(IConfiguration Configuration, MyContext myContext)
        {
            _config = Configuration;
            _myContext = myContext;
        }
        
        [HttpPost("Login")]       
        public IActionResult Login([FromBody]Login login)
        {
            
            bool resultado = Validar(login.Usuario,login.Senha);
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

        private bool Validar(string usuario, string senha)
        {
            var user = _myContext.Usuarios.Where(x => x.Usuario_ == usuario && x.Senha == senha)?.First();

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
