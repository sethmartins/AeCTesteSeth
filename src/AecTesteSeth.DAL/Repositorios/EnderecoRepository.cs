using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace AeCTesteSeth.DAL.Repositorios
{
    public class EnderecoRepository : GenericRepository<Endereco>, IEnderecoRepository
    {
        readonly MyContext _context;
        public EnderecoRepository(MyContext context) : base(context)
        {
            _context = context;
        }

        public  IEnumerable<Endereco> GetEnderecosPorUsuario(int UsuarioId)
        {
           var enderecos =  _context.Enderecos.Where(x => x.UsuarioId == UsuarioId);
            if(enderecos == null)
            {
                throw new ArgumentNullException(nameof(UsuarioId));
            }
            else
            {
                return enderecos;
            }

        }
        public Endereco Get(int id)
        {
            return base.GetAll().FirstOrDefault<Endereco>(x => x.Id == id);
        }
    }
}
