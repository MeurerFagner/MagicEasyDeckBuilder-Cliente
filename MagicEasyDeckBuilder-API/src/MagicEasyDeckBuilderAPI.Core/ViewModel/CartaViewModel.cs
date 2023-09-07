using System;
using System.Collections.Generic;

namespace MagicEasyDeckBuilderAPI.Core.ViewModel
{
    public class CartaViewModel
    {
        public Guid IdCarta { get; set; }
        public string IdScryfall { get; set; }
        public string Nome { get; set; }
        public string NomeOriginal { get; set; }
        public string Texto { get; set; }
        public string TextoOriginal { get; set; }
        public string Tipo { get; set; }
        public string Raridade { get; set; }
        public IEnumerable<string> Cores { get; set; }
        public IEnumerable<string> IdentidadeDeCor { get; set; }
        public IEnumerable<string> Keywords { get; set; }
        public string CustoMana { get; set; }
        public string UrlImage { get; set; }
        public string UrlCropImage { get; set; }
        public string UrlApi { get; set; }
        public string Layout { get; set; }
        public bool CardDuplo { get; set; }
        public ICollection<CartaFaceViewModel> Faces { get; set; }
        public string Poder { get; set; }
        public string Resistencia { get; set; }
        public string Lealdade { get; set; }

    }
}