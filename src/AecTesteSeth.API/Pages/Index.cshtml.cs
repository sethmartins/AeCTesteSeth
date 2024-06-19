using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AeCTesteSeth.API.Data;
using AeCTesteSeth.BLL.Models;

namespace AeCTesteSeth.API.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AeCTesteSeth.API.Data.AeCTesteSethAPIContext _context;

        public IndexModel(AeCTesteSeth.API.Data.AeCTesteSethAPIContext context)
        {
            _context = context;
        }

        public IList<Usuario> Usuario { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Usuario = await _context.Usuario.ToListAsync();
        }
    }
}
