using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Json;
using WebsiteLomba.Models;
using WebsiteLomba.ViewModels;

namespace WebsiteLomba.Controllers
{
    public class PesertaController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _pesertaApiUrl;

        public object PesertaApiBaseUrl { get; private set; }

        public PesertaController(IHttpClientFactory httpClientFactory, IConfiguration configuration ,IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClientFactory.CreateClient();
            _webHostEnvironment = webHostEnvironment;
            _pesertaApiUrl = configuration["PesertaApiBaseUrl"];
        }
        public async Task<IActionResult> Index()
        {
            var pesertaList = await _httpClient.GetFromJsonAsync<List<Peserta>>($"{_pesertaApiUrl}");
            return View(pesertaList);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var peserta = await _httpClient.GetFromJsonAsync<Peserta>($"{PesertaApiBaseUrl}/{id}");
            if (peserta == null)
            {
                return NotFound();
            }
            return View(peserta);
        }
        public async Task<IActionResult> Create()
        {
            var lombaList = await _httpClient.GetFromJsonAsync<List<Lomba>>($"{_pesertaApiUrl}");
            var viewModel = new PesertaCreateViewModel
            {
                peserta = new Peserta(),
                Lombas = lombaList ?? new List<Lomba>()
            };
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PesertaCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.peserta.TanggalDaftar = DateTime.SpecifyKind(model.peserta.TanggalDaftar, DateTimeKind.Utc);

                var response = await _httpClient.PostAsJsonAsync($"{_pesertaApiUrl}", model.peserta);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Gagal menambahkan peserta. Silakan coba lagi.");
                }
                model.Lombas = await _httpClient.GetFromJsonAsync<List<Lomba>>($"{_pesertaApiUrl}");

            }
            return View(model);
        }

        public async Task<IActionResult> Dashboard()
        {
            var pesertaList = await _httpClient.GetFromJsonAsync<List<Peserta>>($"{_pesertaApiUrl}");
            int totalPeserta = pesertaList.Count;
            
            ViewData["TotalPeserta"] = totalPeserta;
            return View();
        }
    }
}
