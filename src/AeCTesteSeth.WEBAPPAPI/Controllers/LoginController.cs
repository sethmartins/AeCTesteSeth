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

namespace AeCTesteSeth.WEBAPPAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly MyContext _work;

        public LoginController([FromServices]MyContext work)
        {
            _work = work;
        }

        // GET: Login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index(Usuario login)
        {
            if (ModelState.IsValid)
            {
                var user = _work.Usuarios.FirstOrDefault<Usuario>(u => u.Usuario_ == login.Usuario_ && u.Senha == login.Senha);

                if (user != null)
                {
                    HttpContext.Session.SetString("Username", user.Usuario_);

                    return RedirectToAction("Index", "Endereco");

                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }

            }

            return View(login);

        }

    }
}
