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
    public class DetailsModel : PageModel
    {
        private readonly AeCTesteSeth.API.Data.AeCTesteSethAPIContext _context;

        public DetailsModel(AeCTesteSeth.API.Data.AeCTesteSethAPIContext context)
        {
            _context = context;
        }

        public Usuario Usuario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                Usuario = usuario;
            }
            return Page();
        }
    }
}
