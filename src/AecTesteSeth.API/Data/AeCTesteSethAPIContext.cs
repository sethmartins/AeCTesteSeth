using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AeCTesteSeth.BLL.Models;

namespace AeCTesteSeth.API.Data
{
    public class AeCTesteSethAPIContext : DbContext
    {
        public AeCTesteSethAPIContext (DbContextOptions<AeCTesteSethAPIContext> options)
            : base(options)
        {
        }

        public DbSet<AeCTesteSeth.BLL.Models.Usuario> Usuario { get; set; } = default!;
    }
}
