using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicEasyDeckBuilderAPI.Core
{
    public class RetornoValidacao
    {
        public bool Valido { get; private set; }
        public IList<string> Erros { get; private set; }

        public RetornoValidacao()
        {
            Valido = true;
            Erros = new List<string>();
        }

        public void AdicionarErro(string erro)
        {
            Valido = false;
            Erros.Add(erro);
        }
    }
}
