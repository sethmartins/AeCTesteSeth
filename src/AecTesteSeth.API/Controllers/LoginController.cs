using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AeCTesteSeth.API.Data;
using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using System.Data.Entity;

namespace AeCTesteSeth.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class _LoginController : Controller
    {
        private readonly MyContext _work;

        public _LoginController(MyContext work)
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
        public async Task<IActionResult> Entrar(Usuario login)
        {
            
                var user = await _work.Usuarios.FirstOrDefaultAsync(u => u.Usuario_ == login.Usuario_ && u.Senha == login.Senha);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(new { message = "Login successful", userId = user.Id });
            

        }

    }
}
