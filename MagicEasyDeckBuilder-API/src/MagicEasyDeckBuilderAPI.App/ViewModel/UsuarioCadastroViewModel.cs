using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicEasyDeckBuilderAPI.App.ViewModel
{
    public class UsuarioCadastroViewModel
    {
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} náo é um e-amil válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O Campo {0} é Obrigatório")]
        public string Senha { get; set; }
        [Compare("Senha",ErrorMessage = "As senhas não conferem")]
        public string SenhaConfirmacao { get; set; }
    }
}
