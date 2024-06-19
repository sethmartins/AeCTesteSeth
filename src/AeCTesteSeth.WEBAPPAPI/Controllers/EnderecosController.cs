using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AeCTesteSeth.WEBAPPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnderecosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet]
        public async Task<IEnumerable<Endereco>> Get()
        {
            return await _unitOfWork.EnderecosRepository.GetAll();

        }

        [HttpGet("endereco")]
        public async Task<IEnumerable<Endereco>> GetByGenero([FromQuery] int Usuarioid)
        {
            return await _unitOfWork.EnderecosRepository.GetEnderecosPorUsuario(Usuarioid);
        }

        // GET api/<Enderecos>/5
        [HttpGet("{id}")]
        public async Task<Endereco> Get(int id)
        {
            return await _unitOfWork.EnderecosRepository.Get(id);
        }

        // POST api/<Enderecos>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Endereco Endereco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _unitOfWork.EnderecosRepository.Add(Endereco);
            _unitOfWork.Commit();

            return new CreatedAtRouteResult("Get", new { id = Endereco.Id }, Endereco);
        }

        // PUT api/<Enderecos>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Endereco Endereco)
        {
            var result = await _unitOfWork.EnderecosRepository.Get(id);

            if (result == null)
                BadRequest();

            if (result.Id != Endereco.Id)
                return BadRequest();

            _unitOfWork.EnderecosRepository.Update(Endereco);
            _unitOfWork.Commit();

            return Ok();
        }

        // DELETE api/<Enderecos>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Endereco = await _unitOfWork.EnderecosRepository.Get(id);

            if (Endereco == null)
                return BadRequest();

            _unitOfWork.EnderecosRepository.Delete(Endereco);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}
