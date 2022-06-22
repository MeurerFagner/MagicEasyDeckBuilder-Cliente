using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Entidades
{
    public class Carta : EntidadeBase
    {
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
        public CustoMana CustoMana { get; set; }
        public string UrlImage { get; set; }
        public string UrlCropImage { get; set; }
        public string UrlApi { get; set; }
        public string Layout { get; set; }
        public bool CardDuplo { get; set; }
        public virtual ICollection<CartaFace> Faces { get; set; }
        public string Poder { get; set; }
        public string Resistencia { get; set; }
        public string Lealdade { get; set; }
        public IDictionary<string, Legalidade> LegalidadePorFormato { get; set; }

        public virtual ICollection<CartaDeck> DeckCartas { get; set; }

        public Carta()
        {
            LegalidadePorFormato = new Dictionary<string, Legalidade>();
            Faces = new List<CartaFace>();
        }

        public IEnumerable<string> GetSupertipos()
        {
            var supertipos = new List<string>();
            if (!string.IsNullOrEmpty(Tipo))
            {
                var tipoCompletoSplited = SeparaTiposDosSubtipos();
                var tipos = tipoCompletoSplited[0].Split(" ");


                foreach (var tipo in tipos.Where(t => SuperTipo.SuperTipos.Contains(t)))
                {
                    supertipos.Add(tipo);
                }
            }
            return supertipos;
        }

        private string[] SeparaTiposDosSubtipos()
        {
            return Tipo.Split('-', '—');
        }

        public IEnumerable<string> GetTipos()
        {
            var tipos = new List<string>();
            if (!string.IsNullOrEmpty(Tipo))
            {
                var tiposSubtipos = SeparaTiposDosSubtipos();

                var tipoSlice = tiposSubtipos[0].Split(" ");
                foreach (var tipo in tipoSlice)
                {
                    if (tipo == TipoCarta.MAGICA_INSTANTEANEA)
                        tipos.Add(TipoCarta.MAGICA_INSTANTEANEA);
                    if (tipo == TipoCarta.FEITICO)
                        tipos.Add(TipoCarta.FEITICO);
                    if (tipo == TipoCarta.CRIATURA)
                        tipos.Add(TipoCarta.CRIATURA);
                    if (tipo == TipoCarta.ARTEFATO)
                        tipos.Add(TipoCarta.ARTEFATO);
                    if (tipo == TipoCarta.ENCANTAMENTO)
                        tipos.Add(TipoCarta.ENCANTAMENTO);
                    if (tipo == TipoCarta.TERRENO)
                        tipos.Add(TipoCarta.TERRENO);
                    if (tipo == TipoCarta.PLANESWALKER)
                        tipos.Add(TipoCarta.PLANESWALKER);
                }
            }
            return tipos;
        }

        public IEnumerable<string> GetSubtipos()
        {
            var subtipos = new List<string>();
            var tiposSubtipos = SeparaTiposDosSubtipos();
            if (tiposSubtipos.Length > 1)
            {
                var subtiposSliced = tiposSubtipos[1].Trim().Split(" ");
                subtipos.AddRange(subtiposSliced);
            }
            return subtipos;
        }

    }
}
