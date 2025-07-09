using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteLomba.Data;
using WebsiteLomba.Models;


namespace WebsiteLomba.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class PesertaApiController : ControllerBase
        {
            private readonly AppDbContext _context;
            public PesertaApiController(AppDbContext context)
            {
                _context = context;
            }
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var pesertaList = await _context.Pesertas
                    .Include(p => p.Lomba) // Include related Lomba data
                    .ToListAsync();
                return Ok(pesertaList);
        }
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] Peserta peserta)
            {
                if (peserta == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                peserta.TanggalDaftar = DateTime.SpecifyKind(peserta.TanggalDaftar, DateTimeKind.Utc);
                _context.Pesertas.Add(peserta);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAll), new { id = peserta.Id }, peserta);
            }
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var peserta = await _context.Pesertas
                    .Include(p => p.Lomba) // Include related Lomba data
                    .FirstOrDefaultAsync(p => p.Id == id);
                if (peserta == null)
                {
                    return NotFound();
                }
                return Ok(peserta);
            }
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] Peserta peserta)
            {
                if (id != peserta.Id || peserta == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var existingPeserta = await _context.Pesertas.FindAsync(id);
                if (existingPeserta == null)
                {
                    return NotFound();
                }
                existingPeserta.NamaPeserta = peserta.NamaPeserta;
                existingPeserta.Email = peserta.Email;
                existingPeserta.NomorTelepon = peserta.NomorTelepon;
                existingPeserta.TanggalDaftar = DateTime.SpecifyKind(peserta.TanggalDaftar, DateTimeKind.Utc);

                _context.Pesertas.Update(existingPeserta);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var peserta = await _context.Pesertas.FindAsync(id);
                if (peserta == null)
                {
                    return NotFound();
                }
                _context.Pesertas.Remove(peserta);
                await _context.SaveChangesAsync();
                return NoContent();
            }

        }
    }

