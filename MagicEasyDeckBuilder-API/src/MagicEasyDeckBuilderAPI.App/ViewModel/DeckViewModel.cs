using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.App.ViewModel
{
    public class DeckViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Formato { get; set; }
        public IEnumerable<string> IdentidadeDeCor { get; set; }
        public IEnumerable<string> Erros { get; set; }

        public IEnumerable<CartaDeckViewModel> MainDeck { get; set; }
        public IEnumerable<CartaDeckViewModel> SideDeck { get; set; }
        public IEnumerable<CartaDeckViewModel> MaybeDeck { get; set; }
        public string Capa { get; set; }

        public DeckViewModel(Guid id, string nome, string formato, IEnumerable<string> identidadeDeCor, string imagemCapaUrl)
        {
            Id = id;
            Nome = nome;
            Formato = formato;
            IdentidadeDeCor = identidadeDeCor;
            Capa = imagemCapaUrl;
        }
        public DeckViewModel()
        {

        }
    }
}
