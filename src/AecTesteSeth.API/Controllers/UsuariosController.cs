using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AeCTesteSeth.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuariosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public  IEnumerable<Usuario> GetUsuarios()
        {
            return  _unitOfWork.UsuarioRepository.GetAll();
           // return resultado;
        }

        [HttpGet("{id}")]
        public  ActionResult<Usuario> GetUsuario(int id)
        {
            var Usuario =  _unitOfWork.UsuarioRepository.Get(id);

            if (Usuario == null)
            {
                return NotFound();
            }

            return Usuario;
        }

        [HttpPut("{id}")]
        public  IActionResult PutUsuario(int id, Usuario Usuario)
        {
            if (id != Usuario.Id)
            {
                return BadRequest();
            }

            var result =  _unitOfWork.UsuarioRepository.Get(id);

            if (result == null)
                BadRequest();

            if (result.Id != Usuario.Id)
                return BadRequest();

            _unitOfWork.UsuarioRepository.Update(Usuario);
            _unitOfWork.Commit();

            return Ok();
        }

        [HttpPost("adicionar")]
        public  ActionResult<Usuario> PostUsuario(Usuario Usuario)
        {
             _unitOfWork.UsuarioRepository.Add(Usuario);
            _unitOfWork.Commit();

            return Ok(new { message = "Salvo com sucesso" });
        }

        [HttpDelete("{id}")]
        public  IActionResult DeleteUsuario(int id)
        {
            var Usuario =  _unitOfWork.UsuarioRepository.Get(id);
            if (Usuario == null)
            {
                return NotFound();
            }

            _unitOfWork.UsuarioRepository.Delete(Usuario);
            _unitOfWork.Commit();

            return NoContent();
        }
    }
}
