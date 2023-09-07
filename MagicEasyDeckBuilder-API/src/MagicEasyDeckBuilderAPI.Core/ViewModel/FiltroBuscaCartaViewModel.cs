using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Core.ViewModel
{
    public class FiltroBuscaCartaViewModel
    {
        public string Nome { get; set; }
        public string Formato { get; set; }
        public IEnumerable<string> Tipos { get; set; }
        public string Raridade { get; set; }
        public string Edicao { get; set; }
        public string CustoMana { get; set; }
        public decimal? ValorMana { get; set; }
        public string IdentidadeDeCor { get; set; }
        public string FiltroDeCor { get; set; }
        public string TipoFiltroCor { get; set; }
        public string Texto { get; set; }
        public int? Page { get; set; }
    }
}
