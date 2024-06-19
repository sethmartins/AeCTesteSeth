using AeCTesteSeth.BLL.Models;

namespace AeCTesteSeth.DOMAIN.DAL.Interfaces
{
    public interface IEnderecoRepository : IGenericRepository<Endereco>
    {
        IEnumerable<Endereco> GetEnderecosPorUsuario(int UsuarioId);
         Endereco Get(int id);
    }
}