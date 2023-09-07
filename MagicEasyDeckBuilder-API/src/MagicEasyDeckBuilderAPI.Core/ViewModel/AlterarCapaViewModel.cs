using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.Core.ViewModel
{
    public class AlterarCapaViewModel
    {
        [Required]
        public Guid IdDeck { get; set; }
        [Required]
        [Url]
        public string UrlImagem { get; set; }
    }
}
