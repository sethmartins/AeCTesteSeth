using AeCTesteSeth.BLL.Models;

namespace AeCTesteSeth.DOMAIN.DAL.Interfaces
{
    public interface IEnderecoRepository : IGenericRepository<Endereco>
    {
        Task<IEnumerable<Endereco>> GetEnderecosPorUsuario(int UsuarioId);
    }
}