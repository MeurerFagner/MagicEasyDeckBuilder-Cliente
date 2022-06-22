using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.App.ViewModel
{
    public class MoverCartaViewModel
    {
        [Required]
        public Guid IdDeck { get; set; }
        [Required]
        public Guid IdCarta { get; set; }
        [Required]
        [RegularExpression("main|side|maybe")]
        public string TipoOrigem { get; set; }
        [RegularExpression("main|side|maybe")]
        [Required]
        public string TipoDestino { get; set; }
    }
}
