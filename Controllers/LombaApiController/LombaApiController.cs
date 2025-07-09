using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteLomba.Data;
using WebsiteLomba.Models;

namespace WebsiteLomba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LombaApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LombaApiController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lombas = await _context.Lombas.ToListAsync();
            return Ok(lombas);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Lomba lomba)
        {
            if (lomba == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            lomba.TanggalMulai = DateTime.SpecifyKind(lomba.TanggalMulai, DateTimeKind.Utc);
            lomba.TanggalSelesai = DateTime.SpecifyKind(lomba.TanggalSelesai, DateTimeKind.Utc);
            _context.Lombas.Add(lomba);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = lomba.Id }, lomba);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lomba = await _context.Lombas.FindAsync(id);
            if (lomba == null)
            {
                return NotFound();
            }
            return Ok(lomba);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Lomba lomba)
        {
            if (id != lomba.Id || lomba == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingLomba = await _context.Lombas.FindAsync(id);
            if (existingLomba == null)
            {
                return NotFound();
            }
            existingLomba.NamaLomba = lomba.NamaLomba;
            existingLomba.Deskripsi = lomba.Deskripsi;
            existingLomba.TanggalMulai = DateTime.SpecifyKind(lomba.TanggalMulai, DateTimeKind.Utc);
            existingLomba.TanggalSelesai = DateTime.SpecifyKind(lomba.TanggalSelesai, DateTimeKind.Utc);
            existingLomba.Lokasi = lomba.Lokasi;
            existingLomba.Kontak = lomba.Kontak;
            existingLomba.LinkPendaftaran = lomba.LinkPendaftaran;
            existingLomba.GambarPath = lomba.GambarPath;
            _context.Lombas.Update(existingLomba);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lomba = await _context.Lombas.FindAsync(id);
            if (lomba == null)
            {
                return NotFound();
            }
            _context.Lombas.Remove(lomba);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
