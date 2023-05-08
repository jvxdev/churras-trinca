using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;

namespace ChurrasTrinca.Pages.Participantes
{
    public class DetailsModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public DetailsModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

      public Participante Participante { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FirstOrDefaultAsync(m => m.Id == id);
            if (participante == null)
            {
                return NotFound();
            }
            else 
            {
                Participante = participante;
            }
            return Page();
        }
    }
}
