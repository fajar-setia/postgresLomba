using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebsiteLomba.Data;
using WebsiteLomba.Models;

namespace WebsiteLomba.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string LombaApiBaseUrl = "http://localhost:5196/api/LombaApi";

        public AdminController(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient();
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var lombas = await _httpClient.GetFromJsonAsync<List<Lomba>>(LombaApiBaseUrl);
            return View(lombas);
        }
        public async Task<IActionResult> Dashboard()
        {
            var lombas = await _httpClient.GetFromJsonAsync<List<Lomba>>(LombaApiBaseUrl);
            int totalLomba = lombas.Count;
            

            ViewData["TotalLomba"] = totalLomba;
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            var lomba = await _httpClient.GetFromJsonAsync<Lomba>($"{LombaApiBaseUrl}/{id}");
            if (lomba == null)
            {
                return NotFound();
            }
            return View(lomba);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lomba lomba, IFormFile gambar)
        {

            if (ModelState.IsValid)
            {
                if (gambar != null && gambar.Length > 0)
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploads);
                    var fileName = Guid.NewGuid() + Path.GetExtension(gambar.FileName);
                    var filePath = Path.Combine(uploads, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await gambar.CopyToAsync(stream);
                    lomba.GambarPath = "/uploads/" + fileName;
                }

                var response = await _httpClient.PostAsJsonAsync(LombaApiBaseUrl, lomba);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }

            return View(lomba);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var lomba = await _httpClient.GetFromJsonAsync<Lomba>($"{LombaApiBaseUrl}/{id}");
            if (lomba == null)
            {
                return NotFound();
            }
            return View(lomba);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lomba lomba, IFormFile gambar)
        {
            if (id != lomba.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (gambar != null && gambar.Length > 0)
                {
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploads);
                    var fileName = Guid.NewGuid() + Path.GetExtension(gambar.FileName);
                    var filePath = Path.Combine(uploads, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await gambar.CopyToAsync(stream);
                    lomba.GambarPath = "/uploads/" + fileName;
                }
                var response = await _httpClient.PutAsJsonAsync($"{LombaApiBaseUrl}/{id}", lomba);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            return View(lomba);
        }
        public async Task<IActionResult> Delete(int id)
        {
           var lomba = await _httpClient.GetFromJsonAsync<Lomba>($"{LombaApiBaseUrl}/{id}");
            if (lomba == null)
            {
                return NotFound();
            }
            return View(lomba);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{LombaApiBaseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to delete the record.");
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("gagal menghapus");
        }
    }
}
