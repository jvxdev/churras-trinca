using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ChurrasTrinca.Pages.Participantes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public Participante Participante { get; set; }

        public Churrasco? Churrasco { get; set; }

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int churrasId)
        {
            ViewData["ChurrascoId"] = new SelectList(_context.Churrascos, "Id", "Nome");

            ViewData["ChurrasId"] = churrasId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Participantes == null || Participante == null)
                return Page();

            _context.Participantes.Add(Participante);
            await _context.SaveChangesAsync();

            TempData["Msg"] = "O participante foi convidado com sucesso!";

            var churrasId = Participante.ChurrascoId;

            ViewData["ChurrasId"] = churrasId;

            return Redirect($"../Churrascos/Edit?id={churrasId}");
        }
    }
}
