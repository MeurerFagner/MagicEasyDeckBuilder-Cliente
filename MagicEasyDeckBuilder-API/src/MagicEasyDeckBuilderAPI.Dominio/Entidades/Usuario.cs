using System;
using System.Collections.Generic;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Dominio.Entidades
{
    public class Usuario: EntidadeBase
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public virtual ICollection<Deck> Decks { get; set; }
    }
}
