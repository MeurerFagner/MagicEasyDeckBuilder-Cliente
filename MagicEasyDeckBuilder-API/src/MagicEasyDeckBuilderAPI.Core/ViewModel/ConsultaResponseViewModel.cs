using System.Collections.Generic;

namespace MagicEasyDeckBuilderAPI.Core.ViewModel
{
    public class ConsultaResponseViewModel
    {
        public bool TemMaisPaginas { get; set; }
        public int TotalDePaginas { get; set; }
        public IEnumerable<CartaViewModel> Cartas { get; set; }
    }
}
