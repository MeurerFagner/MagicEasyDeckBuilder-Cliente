using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.App.ViewModel
{
    public class IncluiDeckViewModel
    {
        [Required(ErrorMessage = "É obrigatório informar o Nome do Deck")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "É obrigatório informar o Formato de jogo do Deck")]
       public string Formato { get; set; }
    }
}
