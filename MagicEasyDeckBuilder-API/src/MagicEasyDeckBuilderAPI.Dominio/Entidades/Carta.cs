using MagicEasyDeckBuilderAPI.Core.Constantes;
using MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Entidades
{
    public class Carta
    {
        public Guid Id { get; set; }
        public string IdScryfall { get; set; }
        public string Nome { get; set; }
        public string Texto { get; set; }
        public string Tipo { get; set; }
        public Raridade Raridade{ get; set; }
        public IEnumerable<Cor> Cores { get; set; }
        public IEnumerable<Cor> IdentidadeDeCor { get; set; }
        public CustoMana CustoMana { get; set; }
        public string UrlImage { get; set; }
        public string UrlCropImage { get; set; }
        public string UrlApi { get; set; }
        public bool CardDuplo { get; set; }
        public Carta OutroLado { get; set; }
        public string Poder { get; set; }
        public string Resistencia { get; set; }
        public int Lealdade { get; set; }
        public IDictionary<string, Legalidade> LegalidadePorFormato { get; set; }

        public Carta()
        {
            LegalidadePorFormato = new Dictionary<string, Legalidade>();
        }

        public IEnumerable<string> GetSupertipos()
        {
            var supertipos = new List<string>();
            if (!string.IsNullOrEmpty(Tipo))
            {
                var tipoCompletoSplited = Tipo.Split("-");
                var tipos = tipoCompletoSplited[0].Split(" ");
                foreach (var tipo in tipos)
                {
                    if (tipo.Contains(Supertipo.LENDARIA) || tipo.Contains(Supertipo.LENDARIO))
                        supertipos.Add(Supertipo.LENDARIA);

                    if (tipo.Contains(Supertipo.BASICO))
                        supertipos.Add(Supertipo.BASICO);

                    if (tipo.Contains(Supertipo.NEVADO))
                        supertipos.Add(Supertipo.NEVADO);
                }
            }
            return supertipos;
        }

        public IEnumerable<string> GetTipos()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetSubtipos()
        {
            throw new NotImplementedException();
        }

    }
}
