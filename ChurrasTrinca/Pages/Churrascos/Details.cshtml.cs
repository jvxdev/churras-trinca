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
using ChurrasTrinca.ViewModels;

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
        public Participante Participante { get; set; } = default!;

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

            if (churrasco.Participantes != null)
            {
                foreach (var participante in churrasco.Participantes.Where(p => p.ChurrascoId == id))

                    Churrasco.SetContribuicaoTotal(participante);
            }

            Churrasco.Participantes = await _context.Participantes.Where(p => p.ChurrascoId == id).Include(x => x.Churrasco).ToListAsync();

            return Page();
        }
    }
}
