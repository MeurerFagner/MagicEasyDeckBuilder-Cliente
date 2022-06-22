using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;

namespace MagicEasyDeckBuilderAPI.Dominio.Entidades
{
    public class CartaFace: EntidadeBase
    {
        public string Nome { get; set; }
        public string NomeOriginal { get; set; }
        public string Texto { get; set; }
        public string TextoOriginal { get; set; }
        public string Tipo { get; set; }
        public IEnumerable<string> Cores { get; set; }
        public string UrlImage { get; set; }
        public string UrlCropImage { get; set; }
        public string Poder { get; set; }
        public string Resistencia { get; set; }
        public string Lealdade { get; set; }
        public Guid IdCarta { get; set; }
        public Carta Carta { get; set; }
        public CustoMana CustoMana { get; set; }


    }
}
