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
    public class DeleteModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public DeleteModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FindAsync(id);

            var churrasId = await _context.Participantes.Select(p => p.ChurrascoId).FirstOrDefaultAsync();

            if (participante != null)
            {
                Participante = participante;
                _context.Participantes.Remove(Participante);
                await _context.SaveChangesAsync();
            }

            TempData["Msg"] = "O participante foi removido com sucesso!";

            return Redirect($"../Churrascos/Edit?id={churrasId}");
        }
    }
}
