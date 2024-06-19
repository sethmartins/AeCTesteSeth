using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AeCTesteSeth.WEBA_PP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]   
    public class EnderecosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnderecosController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        [HttpGet("all")]
        public IEnumerable<Endereco> Get()
        {
            return  _unitOfWork.EnderecosRepository.GetAll();

        }

        [HttpGet("enderecoporusuario")]
        public  IEnumerable<Endereco> GetByUsuario([FromQuery] int Usuarioid)
        {
            return  _unitOfWork.EnderecosRepository.GetEnderecosPorUsuario(Usuarioid);
        }

        // GET api/<Enderecos>/5
        [HttpGet("{id}")]
        public  Endereco Get(int id)
        {
            return  _unitOfWork.EnderecosRepository.Get(id);
        }

        // POST api/<Enderecos>
        [HttpPost("adicionar")]
        public IActionResult Post([FromBody] Endereco endereco)
        {
            if (!ModelState.IsValid)
                return BadRequest();

             _unitOfWork.EnderecosRepository.Add(endereco);
            _unitOfWork.Commit();
            return Ok(new { message = "Salvo com sucesso" }) ;
            //return new CreatedAtRouteResult("Get", new { id = Endereco.Id }, Endereco);
        }

        // PUT api/<Enderecos>/5
        [HttpPut("{id}")]
        public  IActionResult Put(int id, [FromBody] Endereco Endereco)
        {
            var result =  _unitOfWork.EnderecosRepository.Get(id);

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
        public IActionResult Delete(int id)
        {
            var Endereco =  _unitOfWork.EnderecosRepository.Get(id);

            if (Endereco == null)
                return BadRequest();

            _unitOfWork.EnderecosRepository.Delete(Endereco);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}
