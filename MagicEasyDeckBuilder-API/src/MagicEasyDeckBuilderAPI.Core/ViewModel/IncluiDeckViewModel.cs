using System.ComponentModel.DataAnnotations;

namespace MagicEasyDeckBuilderAPI.Core.ViewModel
{
    public class IncluiDeckViewModel
    {
        [Required(ErrorMessage = "É obrigatório informar o Nome do Deck")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "É obrigatório informar o Formato de jogo do Deck")]
        public string Formato { get; set; }
    }
}
