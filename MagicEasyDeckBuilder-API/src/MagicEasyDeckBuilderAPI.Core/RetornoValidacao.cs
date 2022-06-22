using System.Collections.Generic;

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
        public RetornoValidacao(string erro)
        {
            Erros = new List<string>();
            AdicionarErro(erro);
        }

        public void AdicionarErro(string erro)
        {
            Valido = false;
            Erros.Add(erro);
        }
    }
}
