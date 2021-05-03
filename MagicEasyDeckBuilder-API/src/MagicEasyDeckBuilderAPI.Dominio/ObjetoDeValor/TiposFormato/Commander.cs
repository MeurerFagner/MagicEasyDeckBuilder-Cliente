using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.ObjetoDeValor.TiposFormato
{
    public class Commander : Brawl
    {
        public Commander(string nome) : base(nome)
        {
        }

        protected override int GetLimiteMinimoDeCartas() => 100;
    }
}
