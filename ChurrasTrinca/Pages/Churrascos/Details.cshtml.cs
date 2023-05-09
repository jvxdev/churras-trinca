using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChurrasTrinca.Pages.Churrascos
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public DetailsModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Churrasco Churrasco { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Churrascos == null)
            {
                return NotFound();
            }

            var churrasco = await _context.Churrascos.FirstOrDefaultAsync(m => m.Id == id);
            if (churrasco == null)
            {
                return NotFound();
            }
            else 
            {
                Churrasco = churrasco;
            }
            return Page();
        }
    }
}
