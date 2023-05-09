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
using Microsoft.AspNetCore.Authorization;

namespace ChurrasTrinca.Pages.Participantes
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public EditModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

        public Churrasco? Churrasco { get; set; } = default!;

        [BindProperty]
        public Participante Participante { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? churrasId)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FirstOrDefaultAsync(m => m.Id == id);

            var churrasco = await _context.Churrascos.FirstOrDefaultAsync(c => c.Id == churrasId);

            if (participante == null)
            {
                return NotFound();
            }

            Participante = participante;

            Churrasco = churrasco;

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

            var churrasco = await _context.Churrascos.FirstOrDefaultAsync(c => c.Id == Participante.ChurrascoId);

            Churrasco = churrasco;

            return Redirect($"../Churrascos/Edit?id={Churrasco.Id}");
        }

        private bool ParticipanteExists(int id)
        {
            return (_context.Participantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
