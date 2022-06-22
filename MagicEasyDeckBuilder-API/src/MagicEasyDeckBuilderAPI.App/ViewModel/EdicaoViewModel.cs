using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.App.ViewModel
{
    public class EdicaoViewModel
    {
        public string Sigla { get; private set; }
        public string Nome { get; private set; }
        public string IconUrl { get; private set; }

        public EdicaoViewModel( string nome, string sigla, string iconUrl)
        {
            IconUrl = iconUrl;
            Nome = nome;
            Sigla = sigla;
        }
    }
}
