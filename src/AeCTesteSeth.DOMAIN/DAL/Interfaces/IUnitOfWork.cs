using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AeCTesteSeth.DOMAIN.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository UsuarioRepository { get; }
        IEnderecoRepository EnderecosRepository { get; }
        int Commit();
        Task Rollback();
    }
}
