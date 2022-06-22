using System.Collections.Generic;

namespace MagicEasyDeckBuilderAPI.Infra.DadosExternos.SkryfallClassesDeRetorno
{
    public class Root
    {
        public string @object { get; set; }
        public int total_cards { get; set; }
        public bool has_more { get; set; }
        public string next_page { get; set; }
        public List<CardData> data { get; set; }
    }


}
