using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteLomba.Models;

namespace WebsiteLomba.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<Lomba> Lombas { get; set; }
        public DbSet<Peserta> Pesertas { get; set; }
    }
}
