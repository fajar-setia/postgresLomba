
using WebsiteLomba.Models;
namespace WebsiteLomba.ViewModels
{
    public class PesertaCreateViewModel
    {
        public Peserta peserta { get; set; } = new Peserta();
        public List<Lomba> Lombas { get; set; } = new List<Lomba>();
    }
}
