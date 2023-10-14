using System;
using System.Collections.Generic;

namespace MagicEasyDeckBuilderAPI.Core.ViewModel
{
    public class DeckViewModel
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public string Nome { get; set; }
        public FormatoViewModel Formato { get; set; }
        public IEnumerable<string> IdentidadeDeCor { get; set; }
        public IEnumerable<string> Erros { get; set; }

        public IEnumerable<CartaDeckViewModel> MainDeck { get; set; }
        public IEnumerable<CartaDeckViewModel> SideDeck { get; set; }
        public IEnumerable<CartaDeckViewModel> MaybeDeck { get; set; }
        public string Capa { get; set; }

        public DeckViewModel()
        {

        }
    }

    public class FormatoViewModel
    {
        public FormatoViewModel(string nome, bool usaComandante, bool possuiSideDeck, int quantidadeMinimaCartas)
        {
            Nome = nome;
            UsaComandante = usaComandante;
            PossuiSideDeck = possuiSideDeck;
            QuantidadeMinimaCartas = quantidadeMinimaCartas;
        }

        public string Nome { get; set; }
        public bool UsaComandante { get; set; }
        public bool PossuiSideDeck { get; set; }
        public int QuantidadeMinimaCartas { get; set; }
    }
}
