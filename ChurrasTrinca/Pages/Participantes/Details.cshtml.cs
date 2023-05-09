using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ChurrasTrinca.Pages.Participantes
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public DetailsModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

      public Participante Participante { get; set; } = default!;

        public Churrasco Churrasco { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FirstOrDefaultAsync(m => m.Id == id);

            var churrasId = _context.Churrascos.Where(c => c.Id == participante.ChurrascoId).Select(c => c.Id).FirstOrDefault();

            var churrasco = await _context.Churrascos.FirstOrDefaultAsync(m => m.Id == churrasId);

            if (participante == null)
            {
                return NotFound();
            }
            else 
            {
                Participante = participante;
                Churrasco = churrasco;
            }

            var nomeChurras = _context.Churrascos.Where(c => c.Id == churrasId).Select(c => c.Nome).FirstOrDefault();

            ViewData["Churrasco"] = nomeChurras;

            return Page();
        }
    }
}
