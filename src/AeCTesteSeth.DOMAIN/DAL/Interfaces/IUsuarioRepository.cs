using AeCTesteSeth.BLL.Models;

namespace AeCTesteSeth.DOMAIN.DAL.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
         Usuario Get(int id);
    }
}