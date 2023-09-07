using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Core.ViewModel
{
    public class TipoViewModel
    {
        public string Categoria { get; private set; }
        public IEnumerable<string> Tipos { get; private set; }

        public TipoViewModel(string categoria, IEnumerable<string> tipos)
        {
            Categoria = categoria;
            Tipos = tipos;
        }

    }
}
