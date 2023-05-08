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
    public class IndexModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public IndexModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Participante> Participante { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Participantes != null)
            {
                Participante = await _context.Participantes
                .Include(p => p.Churrasco).ToListAsync();
            }
        }
    }
}
