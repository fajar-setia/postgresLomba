namespace WebsiteLomba.Models
{
    public class Lomba
    {
        public int Id { get; set; }
        public string NamaLomba { get; set; } = string.Empty;
        public string Deskripsi { get; set; } = string.Empty;
        public DateTime TanggalMulai { get; set; }
        public DateTime TanggalSelesai { get; set; }
        public string Lokasi { get; set; } = string.Empty;
        public string Kontak { get; set; } = string.Empty;
        public string LinkPendaftaran { get; set; } = string.Empty;
        public string? GambarPath { get;  set; }
    }
}
