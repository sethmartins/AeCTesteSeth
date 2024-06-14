using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeCTesteSeth.DAL.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _context;
        public IUsuarioRepository UsuarioRepository { get; }

        public IEnderecoRepository EnderecosRepository { get; }
        public UnitOfWork(MyContext context, IEnderecoRepository enderecos,
            IUsuarioRepository usuarios)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            UsuarioRepository =  usuarios ?? throw new ArgumentNullException(nameof(usuarios));
            EnderecosRepository = enderecos ?? throw new ArgumentNullException(nameof(enderecos));
        }
        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
