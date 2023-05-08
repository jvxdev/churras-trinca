using ChurrasTrinca.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChurrasTrinca.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {
        }

        public DbSet<Participante> Participantes { get; set; }

        public DbSet<Churrasco> Churrascos { get; set; }
    }
}
