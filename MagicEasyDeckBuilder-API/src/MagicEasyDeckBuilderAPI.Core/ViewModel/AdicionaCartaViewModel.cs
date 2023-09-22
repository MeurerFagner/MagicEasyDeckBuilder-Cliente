using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Core.ViewModel
{
    public class AdicionaCartaViewModel
    {
        [Required]
        public Guid IdDeck { get; set; }
        [Required]
        public string IdScryFall { get; set; }
        [Required]
        [RegularExpression("main|side|maybe|comandante")]
        public string Tipo { get; set; }

        public AdicionaCartaViewModel()
        {
            
        }

        public AdicionaCartaViewModel(Guid idDeck, string idScryFall, string tipo)
        {
            IdDeck = idDeck;
            IdScryFall = idScryFall;
            Tipo = tipo;
        }
    }
}
