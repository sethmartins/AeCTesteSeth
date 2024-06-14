using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;


namespace AeCTesteSeth.DAL.Repositorios
{
    public class EnderecoRepository : GenericRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MyContext context) : base(context)
        {
        }

        public Task<IEnumerable<Endereco>> GetEnderecosPorUsuario(int UsuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
