using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using AeCTesteSeth.WEBAPPAPI.Data;
using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;

using AeCTesteSeth.DOMAIN.DAL.Interfaces;
//using System.Data.Entity;
using AeCTesteSeth.DAL.Repositorios;

namespace AeCTesteSeth.WEB_APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _work;

        public LoginController(IUnitOfWork work)
        {
            _work = work;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost("entrar")]
        public  IActionResult Entrar([FromBody]Login login)
        {
           
                var user =  _work.UsuarioRepository.GetAll().FirstOrDefault<Usuario>(u => u.Usuario_ == login.Usuario && u.Senha == login.Senha);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(new { message = "Login successful", userId = user.Id , userUsuario = user.Usuario_, userName = user.Nome , userSenha= user.Senha});

        }

    }
}
