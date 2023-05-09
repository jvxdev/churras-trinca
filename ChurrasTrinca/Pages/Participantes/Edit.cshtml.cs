using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;

namespace ChurrasTrinca.Pages.Participantes
{
    public class EditModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public EditModel(ChurrasTrinca.Data.AppDbContext context)
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

            Participante = participante;

            ViewData["ChurrascoId"] = new SelectList(_context.Churrascos, "Id", "Descricao");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                TempData["Msg"] = "O participante foi editado com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(Participante.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var churrasId = await _context.Participantes.Where(p => p.Id == Participante.Id).Select(p => p.ChurrascoId).FirstOrDefaultAsync();

            return Redirect($"../Churrascos/Edit?id={churrasId}");
        }

        private bool ParticipanteExists(int id)
        {
            return (_context.Participantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
