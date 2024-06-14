using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;


namespace AeCTesteSeth.DAL.Repositorios
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MyContext context) : base(context)
        {
        }
    }
}
