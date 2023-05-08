using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;

namespace ChurrasTrinca.Pages
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
            return Page();
        }

        [BindProperty]
        public Churrasco Churrasco { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Churrascos == null || Churrasco == null)
            {
                return Page();
            }

            _context.Churrascos.Add(Churrasco);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
