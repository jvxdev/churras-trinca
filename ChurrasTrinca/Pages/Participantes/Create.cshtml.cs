using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;

namespace ChurrasTrinca.Pages.Participantes
{
    public class CreateModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public CreateModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ChurrascoId"] = new SelectList(_context.Churrascos, "Id", "Nome");
            return Page();
        }

        [BindProperty]
        public Participante Participante { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Participantes == null || Participante == null)
            {
                return Page();
            }

            _context.Participantes.Add(Participante);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
