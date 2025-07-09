namespace WebsiteLomba.Models
{
    public class Peserta
    {
        public int Id { get; set; }
        public string NamaPeserta { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NomorTelepon { get; set; } = string.Empty;
        public DateTime TanggalDaftar { get; set; } = DateTime.UtcNow;
        public int LombaId { get; set; }
        public Lomba? Lomba { get; set; }
    }
}
