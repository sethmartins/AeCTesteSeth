using AeCTesteSeth.DAL.Context;
using AeCTesteSeth.DOMAIN.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace AeCTesteSeth.DAL.Repositorios
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MyContext _context;

        protected GenericRepository(MyContext context)
        {
            _context = context;
        }

        public  T Get(int id)
        {
            return  _context.Set<T>().Find(id);
        }

        public  IEnumerable<T> GetAll()
        {
            return  _context.Set<T>().ToList();
        }

        public  void Add(T entity)
        {
             _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
