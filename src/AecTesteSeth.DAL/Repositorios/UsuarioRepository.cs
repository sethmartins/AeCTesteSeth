using AeCTesteSeth.BLL.Models;
using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace AeCTesteSeth.DAL.Repositorios
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MyContext context) : base(context)
        {
        }

        public Usuario Get(int id)
        {
            return base.GetAll().FirstOrDefault<Usuario>(x=>x.Id==id);
        }
    }
}
