using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.App.ViewModel
{
    public class CartaDeckViewModel
    {
        public int Quantidade { get; set; }
        public bool Comandante { get; set; }
        public IEnumerable<string> Erros { get; set; }
        public CartaViewModel Carta { get; set; }

    }
}
