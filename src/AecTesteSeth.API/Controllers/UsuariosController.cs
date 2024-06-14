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
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            var resultado = await _unitOfWork.UsuarioRepository.GetAll();
            return resultado;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var Usuario = await _unitOfWork.UsuarioRepository.Get(id);

            if (Usuario == null)
            {
                return NotFound();
            }

            return Usuario;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario Usuario)
        {
            if (id != Usuario.Id)
            {
                return BadRequest();
            }

            var result = await _unitOfWork.UsuarioRepository.Get(id);

            if (result == null)
                BadRequest();

            if (result.Id != Usuario.Id)
                return BadRequest();

            _unitOfWork.UsuarioRepository.Update(Usuario);
            _unitOfWork.Commit();

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario Usuario)
        {
            await _unitOfWork.UsuarioRepository.Add(Usuario);
            _unitOfWork.Commit();

            return CreatedAtAction("GetUsuario", new { id = Usuario.Id }, Usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var Usuario = await _unitOfWork.UsuarioRepository.Get(id);
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
